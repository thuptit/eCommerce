using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using eCommerce.Shared.Cores.Extensions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Shared.Cores.DataFilters;

public static class QueryExtensions
{
    public static async Task<PagingBase<T>> GetPagingResultAsync<T>(this IQueryable<T> query,GridParam gridParam) 
        where T : class
    {
        query = query.ApplyFilterSearch(gridParam);
        var paging = await query.Skip(gridParam.PageSize * gridParam.PageIndex).Take(gridParam.PageSize).ToListAsync();
        var totalCount = await query.CountAsync();
        return new PagingBase<T>(paging,totalCount);
    }
    private static IQueryable<T> ApplyFilterSearch<T>(this IQueryable<T> query, GridParam gridParam)
    {
        var searchTerm = gridParam.SearchText.EmptyIfNull().Trim();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            var searchFitlers = typeof(T).GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(ApplySearchAttribute), true).Any())
                .Select(x => new ExpressionFilter
                {
                    Comparision = ComparisionOperator.Contains,
                    Value = searchTerm,
                    PropertyName = x.Name,
                    PropertyType = x.PropertyType
                })
                .ToList();
            
            if (searchFitlers.Count == 0 )
                goto ContinueLogic;
            
            var searchExpression = CombineSearch(query, searchTerm, searchFitlers);
            query = query.Where(searchExpression);
        }
        
        ContinueLogic:
        if (!string.IsNullOrEmpty(gridParam.Sort) && !string.IsNullOrEmpty(gridParam.SortDirection))
        {
            query = query.OrderBy(gridParam.Sort, gridParam.SortDirection);
        }
        return query;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty, string sortOrder)
    {
        var type = typeof(T);
        var parameter = Expression.Parameter(type, "x");
        var property = Expression.PropertyOrField(parameter, sortProperty);
        var methodName = sortOrder == "ASC" ? "OrderBy" : "OrderByDescending";
        var ex = Expression.Lambda(property, parameter);
        var typeArguments = new Type[] { type, property.Type };
        var result = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression,Expression.Quote(ex));
        return source.Provider.CreateQuery<T>(result);
    }

    private static Expression<Func<T, bool>> CombineSearch<T>(IQueryable<T> query, string searchTerm,
        List<ExpressionFilter> filters)
    {
        var typeOfDto = typeof(T);
        var parameter = Expression.Parameter(typeOfDto, "x");
        Expression orCondition = null;
        foreach (var filter in filters)
        {
            var conditionExpression = Expression.Call(
                Expression.PropertyOrField(parameter, filter.PropertyName),
                ExpressionRetriver.containsMethod,
                Expression.Constant(searchTerm)
            );
            orCondition = orCondition == null
                ? conditionExpression
                : Expression.OrElse(orCondition, conditionExpression);
        }

        return Expression.Lambda<Func<T, bool>>(orCondition, parameter);
    }
}