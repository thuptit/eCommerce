namespace eCommerce.EntityFrameworkCore.Audits;

public interface IEntity<TPrimaryKey> : ICreationAudit, IDeletionAudit, IModificationAudit, IPrimaryKey<TPrimaryKey>
{
}