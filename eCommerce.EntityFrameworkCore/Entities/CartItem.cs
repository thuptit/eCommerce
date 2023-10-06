using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class CartItem : EntityBase<long>
{
    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Products { get; set; }
    public long UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User Users { get; set; }
    
    public int Quantity { get; set; }
}