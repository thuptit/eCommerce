using Microsoft.EntityFrameworkCore;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public static class UnitOfWorkExtensions
{
    public static Task<TDbContext> GetDbContextAsync<TDbContext>(this IActiveUnitOfWork unitOfWork, string name = null)
        where TDbContext : DbContext
    {
        //if (unitOfWork == null)
            throw new ArgumentNullException("unitOfWork");
        //if(!(unitOfWork is EfCo))
        //ret
    }
}