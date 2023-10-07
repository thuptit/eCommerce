using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;
using eCommerce.Shared.Enums;

namespace eCommerce.EntityFrameworkCore.Entities;

public class Order : EntityBase<long>
{
    public long UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User Users { get; set; }
    public int Total { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}