using eCommerce.EntityFrameworkCore.Cores.Uow;

namespace eCommerce.EntityFrameworkCore.Cores;

public class AsyncLocalUnitOfWorkProvider : ICurrentUnitOfWorkProvider
{
    public IUnitOfWorkCore Current { get => GetCurrentUow(); set => SetCurrentUow(value); }
    private static readonly AsyncLocal<LocalUowWrapper> AsyncLocalUow = new();

    public AsyncLocalUnitOfWorkProvider()
    {
    }

    private static IUnitOfWorkCore GetCurrentUow()
    {
        var uow = AsyncLocalUow.Value?.UnitOfWork;
        if (uow == null)
        {
            return null;
        }

        if (uow.IsDispose)
        {
            AsyncLocalUow.Value = null;
            return null;
        }

        return uow;
    }

    private static void SetCurrentUow(IUnitOfWorkCore value)
    {
        lock (AsyncLocalUow)
        {
            if (value == null)
            {
                if (AsyncLocalUow.Value == null)
                {
                    return;
                }

                if (AsyncLocalUow.Value.UnitOfWork?.Outer == null)
                {
                    AsyncLocalUow.Value.UnitOfWork = null;
                    AsyncLocalUow.Value = null;
                    return;
                }

                AsyncLocalUow.Value.UnitOfWork = AsyncLocalUow.Value.UnitOfWork.Outer;
            }
            else
            {
                if (AsyncLocalUow.Value?.UnitOfWork == null)
                {
                    if (AsyncLocalUow.Value != null)
                    {
                        AsyncLocalUow.Value.UnitOfWork = value;
                    }

                    AsyncLocalUow.Value = new LocalUowWrapper(value);
                    return;
                }

                value.Outer = AsyncLocalUow.Value.UnitOfWork;
                AsyncLocalUow.Value.UnitOfWork = value;
            }
        }
    }
    private class LocalUowWrapper
    {
        public IUnitOfWorkCore UnitOfWork { get; set; }

        public LocalUowWrapper(IUnitOfWorkCore unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
