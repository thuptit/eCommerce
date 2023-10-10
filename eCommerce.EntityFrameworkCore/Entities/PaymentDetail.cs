using eCommerce.EntityFrameworkCore.Audits;
using eCommerce.Shared.Enums;

namespace eCommerce.EntityFrameworkCore.Entities;

public class PaymentDetail : EntityBase<long>
{
    public decimal Amount { get; set; }
    public string Provider { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}