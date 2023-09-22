namespace eCommerce.EntityFrameworkCore.Audits;

public interface IDeletionAudit : ISoftDelete
{
    public long? DeletorId { get; set; }
    public DateTime? DeletionTime { get; set; }
}