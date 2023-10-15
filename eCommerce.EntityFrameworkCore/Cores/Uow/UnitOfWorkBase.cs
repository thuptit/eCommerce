using System.Collections.Immutable;
using eCommerce.EntityFrameworkCore.Cores.Arguments;
using eCommerce.EntityFrameworkCore.Cores.Options;
using Volo.Abp;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public abstract class UnitOfWorkBase : IUnitOfWorkCore
{
    public string Id { get; }
    public IUnitOfWorkCore Outer { get; set; }

    public event EventHandler? Completed;
    public event EventHandler<UnitOfWorkFailedEventArgs>? Failed;
    public event EventHandler? Dispose;
    public UnitOfWorkOptions Options { get; private set; }
    public IReadOnlyList<DataFilterConfiguration> Filters => _filters.ToImmutableList();
    private readonly List<DataFilterConfiguration> _filters;
    public Dictionary<string, object> Items { get; set; }
    public bool IsDispose { get; private set; }
    protected IUnitOfWorkFilterExecuter FilterExecuter { get; set; }
    protected IUnitOfWorkDefaultOptions DefaultOptions { get; set; }
    private bool _isBeginCalledBefore;
    private bool _isCompleteCalledBefore;
    private bool _succeed;
    private Exception _exception;
    public UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions, IUnitOfWorkFilterExecuter filterExecuter)
    {
        DefaultOptions = defaultOptions;
        FilterExecuter = filterExecuter;

        Id = Guid.NewGuid().ToString("N");
        _filters = defaultOptions.Filters.ToList();
        Items = new();
    }
    
    public void Begin(UnitOfWorkOptions options)
    {
        Check.NotNull(options, nameof(options));
        PreventMultipleBegin();
        Options = options;
        SetFilters(options.FilterOverrides);
        BeginUow();
    }

    protected virtual void BeginUow()
    {
    }

    protected abstract void CompleteUow();
    protected abstract Task CompleteUowAsync();
    protected abstract void DisposeUow();
    private void PreventMultipleBegin()
    {
        if (_isBeginCalledBefore)
        {
            throw new Exception("The unit of work has started before. Can not call Start method more than once");
        }

        _isBeginCalledBefore = true;
    }

    private void SetFilters(List<DataFilterConfiguration> filterOverrides)
    {
        _filters.ForEach(filter =>
        {
            var filterOverride = filterOverrides.FirstOrDefault(x => x.FilterName == filter.FilterName);
            if (filterOverride != null)
            {
                filter = filterOverride;
            }
        });
    }

    public abstract void SaveChanges();

    public abstract Task SaveChangesAsync();

    public IDisposable DisableFilter(params string[] filterNames)
    {
        throw new NotImplementedException();
    }

    public IDisposable EnableFilter(params string[] filterNames)
    {
        throw new NotImplementedException();
    }

    public bool IsFilterEnabled(string filterName)
    {
        throw new NotImplementedException();
    }

    public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
    {
        throw new NotImplementedException();
    }

    public IDisposable DisableAuditing(params string[] filterNames)
    {
        throw new NotImplementedException();
    }

    public IDisposable EnableAuditing(params string[] filterNames)
    {
        throw new NotImplementedException();
    }

    void IDisposable.Dispose()
    {
        throw new NotImplementedException();
    }

    public void Complete()
    {
        throw new NotImplementedException();
    }

    public Task CompleteAsync()
    {
        throw new NotImplementedException();
    }
}