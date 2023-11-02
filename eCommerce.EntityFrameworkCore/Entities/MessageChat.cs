using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.EntityFrameworkCore.Entities;

public class MessageChat : EntityBase<long>
{
    public string Message { get; set; }
    public long GroupChatId { get; set; }
    [ForeignKey(nameof(GroupChatId))]
    public virtual GroupChat GroupChat { get; set; }
    public long SenderId { get; set; }
    [ForeignKey(nameof(SenderId))]
    public virtual User Sender { get; set; }

    public bool IsSeen { get; set; }
    public DateTime? SeenTime { get; set; }
}