using eCommerce.EntityFrameworkCore.Cores.Arguments;
using eCommerce.EntityFrameworkCore.Cores.Options;
using eCommerce.EntityFrameworkCore.Cores.Uow;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public interface IActiveUnitOfWork
{
    event EventHandler Completed;
    event EventHandler<UnitOfWorkFailedEventArgs> Failed;
    event EventHandler Dispose;
    UnitOfWorkOptions Options { get; }
    IReadOnlyList<DataFilterConfiguration> Filters { get; }
    Dictionary<string, object> Items { get; set; }
    bool IsDispose { get; }
    void SaveChanges();
    Task SaveChangesAsync();
    IDisposable DisableFilter(params string[] filterNames);
    IDisposable EnableFilter(params string[] filterNames);
    bool IsFilterEnabled(string filterName);
    IDisposable SetFilterParameter(string filterName, string parameterName, object value);
    /// <summary>
    /// Disable automactic saving for one or more audit fields
    /// </summary>
    /// <param name="filterNames"></param>
    /// <returns></returns>
    IDisposable DisableAuditing(params string[] filterNames);
    IDisposable EnableAuditing(params string[] filterNames);
}