namespace eCommerce.EntityFrameworkCore.Cores.Uow;

/// <summary>
/// Used to Complete a unit of work
/// </summary>
public interface IUnitOfWorkCompleteHandle : IDisposable
{
    void Complete();
    Task CompleteAsync();
}