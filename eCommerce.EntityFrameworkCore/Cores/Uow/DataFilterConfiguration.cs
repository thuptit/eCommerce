namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public class DataFilterConfiguration
{
    public string FilterName { get; set; }
    public bool IsEnable { get; set; }
    public IDictionary<string,object> FilterParameters { get; set; }

    public DataFilterConfiguration(string filterName, bool isEnable)
    {
        FilterName = filterName;
        IsEnable = isEnable;
    }

    internal DataFilterConfiguration(DataFilterConfiguration filterToClone, bool? isEnable = null)
        : this(filterToClone.FilterName, isEnable ?? filterToClone.IsEnable)
    {
        foreach (var filterParameter in FilterParameters)
        {
            FilterParameters[filterParameter.Key] = filterParameter.Value;
        }
    }
}