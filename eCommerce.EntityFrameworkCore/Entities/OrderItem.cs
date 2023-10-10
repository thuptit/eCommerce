using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class OrderItem : EntityBase<long>
{
    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal Discount { get; set; }
    public decimal Price { get; set; }
}