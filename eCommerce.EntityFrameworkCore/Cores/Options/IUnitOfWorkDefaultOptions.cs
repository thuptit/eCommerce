using System.Transactions;
using eCommerce.EntityFrameworkCore.Cores.Uow;

namespace eCommerce.EntityFrameworkCore.Cores.Options;

public interface IUnitOfWorkDefaultOptions
{
    TransactionScopeOption Scope { get; set; }
    bool IsTransactional { get; set; }
    bool IsTransactionScopeAvailable { get; set; }
    TimeSpan? Timeout { get; set; }
    IsolationLevel? IsolationLevel{get; set; }
    IReadOnlyList<DataFilterConfiguration> Filters { get; }
    List<Func<Type, bool>> ConvetionalUowSelectors { get; }
    void RegisterFilter(string filterName, bool isEnableByDefault);
}