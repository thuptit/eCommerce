using eCommerce.EntityFrameworkCore.Audits;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.EntityFrameworkCore.Entities;

public class PersonalChat : EntityBase<long>
{
    public long UserA_Id { get; set; }
    [ForeignKey(nameof(UserA_Id))]
    public virtual User UserA { get; set; }
    public long UserB_Id { get; set; }
    [ForeignKey(nameof(UserB_Id))]
    public virtual User UserB { get; set; }

    public virtual ICollection<MessageChatPersonal> MessageChatPersonals { get; set; }

}