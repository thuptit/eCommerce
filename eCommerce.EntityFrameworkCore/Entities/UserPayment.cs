using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;
using eCommerce.Shared.Enums;

namespace eCommerce.EntityFrameworkCore.Entities;

public class UserPayment : EntityBase<long>
{
    public long UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    public PaymentType PaymentType { get; set; }
    public string Provider { get; set; }
    public string AccountNo { get; set; }
    public DateTime Expiry { get; set; }
}