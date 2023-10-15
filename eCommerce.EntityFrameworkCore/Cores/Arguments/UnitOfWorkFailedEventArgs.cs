namespace eCommerce.EntityFrameworkCore.Cores.Arguments;

public class UnitOfWorkFailedEventArgs : EventArgs
{
    public Exception Exception { get; set; }

    public UnitOfWorkFailedEventArgs(Exception exception)
    {
        Exception = exception;
    }
}