using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using eCommerce.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Audits;
using eCommerce.EntityFrameworkCore.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Domain.Repositories;

public class Repository<TEntity,TPrimaryKey> : IRepository<TEntity,TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly eCommerceDbContext _context;

    public IUnitOfWork CurrentUnitOfWork => _context;
    
    public Repository(eCommerceDbContext context)
    {
        _context = context;
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
        return _dbSet.FirstOrDefault(x => id.Equals(x.Id));
    }

    public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => id.Equals(x.Id));
    }

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public TEntity Load(TPrimaryKey id)
    {
        throw new NotImplementedException();
    }

    public TEntity Insert(TEntity entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public TPrimaryKey InsertAndGetId(TEntity entity)
    {
        var newEntity = _dbSet.Add(entity);
        CurrentUnitOfWork.SaveChanges();
        return newEntity.Entity.Id;
    }

    public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        var newEntity = await _dbSet.AddAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();
        return newEntity.Entity.Id;
    }

    public TEntity InsertOrUpdate(TEntity entity)
    {
        if (entity.Id.Equals(0))
        {
            return Insert(entity);
        }

        return Update(entity);
    }

    public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
    {
        if (entity.Id.Equals(0))
        {
            return await InsertAsync(entity);
        }

        return await UpdateAsync(entity);
    }

    public TEntity Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
    {
        var entity = _dbSet.Find(id);
        updateAction(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
    {
        var entity = await _dbSet.FindAsync(id);
        var task =  updateAction(entity);
        await task;
        return entity;
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public void Delete(TPrimaryKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            throw new NullReferenceException("Not found Entity");
        }

        entity.IsDeleted = true;
    }

    public async Task DeleteAsync(TPrimaryKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            throw new NullReferenceException("Not found Entity");
        }

        entity.IsDeleted = true;
    }

    public int Count()
    {
        return _dbSet.Count();
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).Count();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).CountAsync();
    }

    public long LongCount()
    {
        return _dbSet.LongCount();
    }

    public async Task<long> LongCountAsync()
    {
        return await _dbSet.LongCountAsync();
    }

    public long LongCount(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.LongCount(predicate);
    }

    public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.LongCountAsync(predicate);
    }
}