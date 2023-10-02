using System.Linq.Expressions;
using System.Reflection;
using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerce.EntityFrameworkCore.Intercepters;

public static class SoftDeleteQueryExtensions
{
    public static void AddSoftDeleteQuery(this IMutableEntityType entityType)
    {
        var methodToCall = typeof(SoftDeleteQueryExtensions)
            .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entityType.ClrType);
        var filter = methodToCall.Invoke(null,new object[] { });
        entityType.SetQueryFilter((LambdaExpression)filter);
        entityType.AddIndex(entityType.FindProperty(nameof(ISoftDelete.IsDeleted)));
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, ISoftDelete
    {
        Expression<Func<TEntity, bool>> filter = x => x.IsDeleted == false;
        return filter;
    }
}