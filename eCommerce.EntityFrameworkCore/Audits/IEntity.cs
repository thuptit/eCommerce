namespace eCommerce.EntityFrameworkCore.Audits;

public interface IEntity<TPrimaryKey> : IEntity, IPrimaryKey<TPrimaryKey>
{
}

public interface IEntity : ICreationAudit, IDeletionAudit, IModificationAudit
{
}