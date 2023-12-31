using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class Product : EntityBase<long>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    
    #region Relationship
    public long CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
    public virtual ProductInventory ProductInventory { get; set; }
    #endregion
}