using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.EntityFrameworkCore.Entities;

public class User : IdentityUser<long>, IEntity<long>
{
    public string Address { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsAdmin { get; set; }
    public long? CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public long? ModifiorId { get; set; }
    public DateTime? ModificationTime { get; set; }
    public long? DeletorId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
}