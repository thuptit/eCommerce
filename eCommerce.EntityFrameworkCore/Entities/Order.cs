using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class Order : EntityBase<long>
{
    public long UserId { get; set; }
    public int Total { get; set; }
    
}