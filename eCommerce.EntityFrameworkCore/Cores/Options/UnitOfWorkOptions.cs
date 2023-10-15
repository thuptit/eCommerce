using System.Transactions;
using eCommerce.EntityFrameworkCore.Cores.Uow;

namespace eCommerce.EntityFrameworkCore.Cores.Options;

public class UnitOfWorkOptions
{
    public TransactionScopeOption? Scope { get; set; }
    public bool? IsTransactional { get; set; }
    public TimeSpan? Timeout { get; set; }
    public IsolationLevel? IsolationLevel { get; set; }
    public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }
    public List<DataFilterConfiguration> FilterOverrides { get; }

    public UnitOfWorkOptions()
    {
        FilterOverrides = new();
    }

    internal void FillDefaultsForNonProviderOptions(IUnitOfWorkDefaultOptions defaultOptions)
    {
        if (!IsTransactional.HasValue)
        {
            IsTransactional = defaultOptions.IsTransactional;
        }

        if (!Scope.HasValue)
        {
            Scope = defaultOptions.Scope;
        }

        if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
        {
            Timeout = defaultOptions.Timeout;
        }

        if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
        {
            IsolationLevel = defaultOptions.IsolationLevel;
        }
    }

    internal void FillOuterUowFiltersForNonProviderOptions(List<DataFilterConfiguration> filterOverrides)
    {
        foreach (var filterOverride in filterOverrides)
        {
            if(FilterOverrides.Any(x => x.FilterName == filterOverride.FilterName))
                continue;
            
            FilterOverrides.Add(filterOverride);
        }
    }
}
