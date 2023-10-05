using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class Category : EntityBase<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}