using eCommerce.EntityFrameworkCore.Audits;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.EntityFrameworkCore.Entities;

public class PersonalChat : EntityBase<long>
{
    public long SenderId { get; set; }
    [ForeignKey(nameof(SenderId))]
    public virtual User Sender { get; set; }
    public long ReceiverId { get; set; }
    [ForeignKey(nameof(ReceiverId))]
    public virtual User Receiver { get; set; }

    public virtual ICollection<MessageChatPersonal> MessageChatPersonals { get; set; }

}