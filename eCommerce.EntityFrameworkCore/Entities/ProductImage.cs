using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class ProductImage : EntityBase<long>
{
    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public string ImageUrl { get; set; }
}