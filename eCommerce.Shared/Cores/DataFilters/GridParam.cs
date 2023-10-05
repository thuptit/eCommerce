using MediatR;

namespace eCommerce.Shared.Cores.DataFilters;

public class GridParam
{
    public string Sort { get; set; }
    public SortDirection SortDirection { get; set; }
    public string SearchText { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}

public enum SortDirection : byte
{
    ASC = 0,
    DESC = 1
}