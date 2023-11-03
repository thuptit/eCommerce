using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class GroupChatUser : EntityBase<long>
{
    public long UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    public long GroupChatId { get; set; }
    [ForeignKey(nameof(GroupChatId))]
    public virtual GroupChat GroupChat { get; set; }
}