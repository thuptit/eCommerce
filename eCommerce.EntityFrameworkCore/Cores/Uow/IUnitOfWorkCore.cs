using eCommerce.EntityFrameworkCore.Cores.Options;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public interface IUnitOfWorkCore : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
{
    string Id { get; }
    IUnitOfWorkCore Outer { get; set; }
    void Begin(UnitOfWorkOptions options);
}