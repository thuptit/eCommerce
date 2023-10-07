using System.ComponentModel.DataAnnotations;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class ProductDiscount : EntityBase<long>
{
    public long ProductId { get; set; }
    public virtual Product Product { get; set; }
    [MaxLength(250)]
    public string Name { get; set; }
    [MaxLength(1000)]
    public string Desciption { get; set; }
    public decimal Discount { get; set; }
    public bool IsActive { get; set; }
}