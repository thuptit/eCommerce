using System.Reflection;
using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Cores.Sessions;
using Microsoft.EntityFrameworkCore.Storage;

namespace eCommerce.EntityFrameworkCore;

public class eCommerceDbContext : IdentityDbContext<User,Role,long>, IEcommerceDbContext
{
    #region custom implementation
    //protected 
    public override int SaveChanges()
    {
        ApplyAudits();
        return base.SaveChanges();
    }

    public void ApplyAudits()
    {
        ChangeTracker.DetectChanges();
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity as IEntity; 
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entity is null)
                continue;

            if (entity is not null)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreationTime = DateTime.Now;
                        entity.CreatorId = _session.UserId;
                        break;
                    case EntityState.Modified:
                        if (entity.IsDeleted)
                        {
                            entity.DeletionTime = DateTime.Now;
                            entity.DeletorId = _session.UserId;
                        }
                        else
                        {
                            entity.ModificationTime = DateTime.Now;
                            entity.ModifiorId = _session.UserId;
                        }
                        break;
                }
            }
            
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyAudits();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyAudits();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private IDbContextTransaction _currentTransaction;
    private IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    private readonly IEcommerceSession _session;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) 
            return null;

        _currentTransaction = await Database.BeginTransactionAsync();
        return _currentTransaction;
    }

    public async Task CommitAsync()
    {
        if (_currentTransaction == null) 
            throw new InvalidOperationException("Current transaction should be not null");
        try
        {
            await SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await RollbackAsync();
            
            async Task RollbackAsync()
            {
                try
                {
                    await _currentTransaction.RollbackAsync();
                }
                catch (Exception ex)
                {
                    //TODO: tracer exception
                }
                
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
    #endregion
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public eCommerceDbContext(DbContextOptions options, IEcommerceSession session) : base(options)
    {
        _session = session;
    }
}