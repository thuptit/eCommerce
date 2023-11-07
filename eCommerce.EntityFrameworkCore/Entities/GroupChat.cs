using eCommerce.EntityFrameworkCore.Audits;
using eCommerce.Shared.Enums;

namespace eCommerce.EntityFrameworkCore.Entities;

public class GroupChat : EntityBase<long>
{
    public string? Name { get; set; }
}