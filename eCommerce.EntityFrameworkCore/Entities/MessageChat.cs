using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class MessageChatGroup : EntityBase<long>
{
    public string Message { get; set; }
    public long GroupChatId { get; set; }
    [ForeignKey(nameof(GroupChatId))]
    public virtual GroupChat GroupChat { get; set; }
    public long SenderId { get; set; }
    public bool IsSeen { get; set; }
    public DateTime? SeenTime { get; set; }
}

public class MessageChatPersonal : EntityBase<long>
{
    public string Message { get; set; }
    public long PersonalChatId { get; set; }
    [ForeignKey(nameof(PersonalChatId))]
    public virtual PersonalChat  PersonalChat { get; set; }
    public long SenderId { get; set; }
    [ForeignKey(nameof(SenderId))]
    public virtual User Sender { get; set; }
    public bool IsSeen { get; set; }
    public DateTime? SeenTime { get; set; }
}