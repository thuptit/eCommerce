using eCommerce.EntityFrameworkCore.Cores.Uow;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.EntityFrameworkCore.Cores;

public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
    where TDbContext: DbContext
{
    private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

    public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
    {
        _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
    }
    public Task<TDbContext> GetDbContextAsync()
    {
        //return _currentUnitOfWorkProvider.Current.Get
        throw new NotImplementedException();
    }

    public TDbContext GetDbContext()
    {
        throw new NotImplementedException();
    }
}