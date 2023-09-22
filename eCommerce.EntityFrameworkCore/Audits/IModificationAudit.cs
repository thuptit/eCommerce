namespace eCommerce.EntityFrameworkCore.Audits;

public interface IModificationAudit
{
    public long? ModifiorId { get; set; }
    public DateTime? ModificationTime { get; set; }
}