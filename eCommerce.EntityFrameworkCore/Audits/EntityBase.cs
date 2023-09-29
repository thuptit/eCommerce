namespace eCommerce.EntityFrameworkCore.Audits;

public abstract class EntityBase<TPrimaryKey> : IEntity<TPrimaryKey>
{
    public TPrimaryKey Id { get; set; }
    public long? CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
    public long? ModifiorId { get; set; }
    public DateTime? ModificationTime { get; set; }
    public long? DeletorId { get; set; }
    public DateTime? DeletionTime { get; set; }
    public bool IsDeleted { get; set; }
}