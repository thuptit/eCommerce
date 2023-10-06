using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class Product : EntityBase<long>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    
    #region Relationship
    public long CategoryId { get; set; }
    public virtual Category Categories { get; set; }
    #endregion
}