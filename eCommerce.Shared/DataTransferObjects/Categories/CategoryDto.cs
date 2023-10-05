using eCommerce.Shared.Cores.DataFilters;

namespace eCommerce.Shared.DataTransferObjects.Categories;

public class CategoryDto
{
    public long Id { get; set; }
    [ApplySearch]
    public string Name { get; set; }
    [ApplySearch]
    public string Description { get; set; }
}