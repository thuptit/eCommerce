namespace eCommerce.Shared.Cores.DataFilters;

public class PagingBase<T> where T : class
{
    public IReadOnlyList<T> Items {get; set; }
    public long TotalCount { get; set; }
    public PagingBase(List<T> _items, long totalCount)
    {
        Items = _items;
        TotalCount = totalCount;
    }
}