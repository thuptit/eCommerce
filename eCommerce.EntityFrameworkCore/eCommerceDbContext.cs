using System.Linq.Expressions;
using System.Reflection;
using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.EntityFrameworkCore.Intercepters;
using eCommerce.EntityFrameworkCore.UnitOfWorks;
using eCommerce.Shared.Cores.Sessions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace eCommerce.EntityFrameworkCore;

public class eCommerceDbContext : IdentityDbContext<User,Role,long>, IUnitOfWork
{
    #region custom implementation
    //protected 
    public override int SaveChanges()
    {
        ApplyAudits();
        return base.SaveChanges();
    }

    private void ApplyAudits()
    {
        ChangeTracker.DetectChanges();
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = (IEntity?)entry.Entity; 
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entity == null)
                continue;
            
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQuery();
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
    private readonly ILogger<eCommerceDbContext> _logger;
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            throw new Exception("Not initiate new transaction when DbContext is existing other transaction. " +
                                "Please commit transaction before create new transaction");
        }
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
            _logger.LogException(ex);
            await RollbackAsync();
            
            async Task RollbackAsync()
            {
                try
                {
                    await _currentTransaction.RollbackAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
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
        _logger = NullLogger<eCommerceDbContext>.Instance;
        ;
    }
}