using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using eCommerce.Shared.Cores.Extensions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Shared.Cores.DataFilters;

public static class QueryExtensions
{
    public static async Task<PagingBase<T>> GetAllPaging<T>(this IQueryable<T> query,GridParam gridParam) 
        where T : class
    {
        query = query.ApplyFilterSearch(gridParam);
        var paging = await query.Take(gridParam.PageSize).Skip(gridParam.PageSize * gridParam.PageIndex).ToListAsync();
        var totalCount = await query.CountAsync();
        return new PagingBase<T>(paging,totalCount);
    }
    private static IQueryable<T> ApplyFilterSearch<T>(this IQueryable<T> query, GridParam gridParam)
    {
        var searchTerm = gridParam.SearchText.EmptyIfNull().Trim().ToLower();
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
            
            var expressionSearchConstant = Expression.Constant(searchTerm.EmptyIfNull().Trim().ToLower());
            var typeOfDto = typeof(T);
            var parameter = Expression.Parameter(typeOfDto, "x");
            Expression orCondition = null;
            foreach (var filter in searchFitlers)
            {
                var property = Expression.PropertyOrField(parameter, filter.PropertyName);
                var conditionExpression =
                    Expression.Call(property, ExpressionRetriver.containsMethod, expressionSearchConstant);
                if (orCondition == null)
                {
                    orCondition = conditionExpression;
                }
                else
                {
                    orCondition = Expression.OrElse(orCondition, conditionExpression);
                }
            }

            query = query.Where(Expression.Lambda<Func<T,bool>>(orCondition,parameter));
        }
        
        ContinueLogic:
        return query;
    }
}