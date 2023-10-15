using Microsoft.EntityFrameworkCore;

namespace eCommerce.EntityFrameworkCore.Cores;

public interface IDbContextProvider<TDbContext>
where TDbContext : DbContext
{
    Task<TDbContext> GetDbContextAsync();
    TDbContext GetDbContext();
}