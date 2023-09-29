namespace eCommerce.EntityFrameworkCore.Audits;

public interface ICreationAudit
{
    public long? CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
}