using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class PersonalChat : EntityBase<long>
{
    public string SenderId { get; set; }
    
}