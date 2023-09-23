using System.Linq.Expressions;
using eCommerce.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Domain.Repositories;

public class Repository<TEntity,TPrimaryKey> : IRepository<TEntity,TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(eCommerceDbContext _context)
    {
        _dbSet = _context.Set<TEntity>();
    }
    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        var query = _dbSet.AsQueryable();
        foreach (var propertySelector in propertySelectors)
        {
            query = query.Include(propertySelector);
        }

        return query;
    }

    public List<TEntity> GetAllList()
    {
        return _dbSet.ToList();
    }

    public async Task<List<TEntity>> GetAllListAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).ToList();
    }

    public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
    {
        throw new NotImplementedException();
    }

    public TEntity Get(TPrimaryKey id)
    {
        return _dbSet.Find(id);
    }

    public async Task<TEntity> GetAsync(TPrimaryKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public TEntity Single(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Single(predicate);
    }

    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.SingleAsync(predicate);
    }

    public TEntity FirstOrDefault(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public TEntity Load(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public TEntity Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TPrimaryKey InsertAndGetId(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity InsertOrUpdate(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertOrUpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public long LongCount()
    {
        throw new NotImplementedException();
    }

    public Task<long> LongCountAsync()
    {
        throw new NotImplementedException();
    }

    public long LongCount(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}