using eCommerce.EntityFrameworkCore.Audits;

namespace eCommerce.Domain.Repositories;

public interface IRepositoryBase<in TEntity, in TPrimaryKey> where TEntity : IEntity<TPrimaryKey>
{
}