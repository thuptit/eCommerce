namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public interface ICurrentUnitOfWorkProvider
{
    IUnitOfWorkCore Current { get; set; }
}