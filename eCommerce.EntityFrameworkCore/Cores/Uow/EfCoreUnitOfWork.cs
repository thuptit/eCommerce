using eCommerce.EntityFrameworkCore.Cores.Options;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public class EfCoreUnitOfWork : UnitOfWorkBase
{
    public EfCoreUnitOfWork(IUnitOfWorkDefaultOptions defaultOptions, IUnitOfWorkFilterExecuter filterExecuter) : base(defaultOptions, filterExecuter)
    {
    }

    protected override void CompleteUow()
    {
        throw new NotImplementedException();
    }

    protected override Task CompleteUowAsync()
    {
        throw new NotImplementedException();
    }

    protected override void DisposeUow()
    {
        throw new NotImplementedException();
    }

    public override void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public override Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}