using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class ProductInventory : EntityBase<long>
{
    public int Quantity { get; set; }
}