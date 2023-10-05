using System.Linq.Expressions;

namespace eCommerce.Shared.Cores.Extensions;

public class ExpressionEx
{
    public static Expression<Func<T>> Expr<T>(Expression<Func<T>> expr) { return expr; }
    public static Expression<Func<TArg1, TRes>> Create<TArg1, TRes>(Expression<Func<TArg1, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TRes>> Create<TArg1, TArg2, TRes>(Expression<Func<TArg1, TArg2, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TRes>> Create<TArg1, TArg2, TArg3, TRes>(Expression<Func<TArg1, TArg2, TArg3, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TArg11, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TArg11, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TArg11, TRes>> expr) { return expr; }
    public static Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TArg11, TArg12, TRes>> Create<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TArg11, TArg12, TRes>(Expression<Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Targ10, TArg11, TArg12, TRes>> expr) { return expr; }
}