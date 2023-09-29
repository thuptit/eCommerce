using System.Reflection;
using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Cores.Sessions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace eCommerce.EntityFrameworkCore;

public class eCommerceDbContext : IdentityDbContext<User,Role,long>
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
        foreach (var entity in ChangeTracker.Entries())
        {
            var typeIEntity = typeof(IEntity<>);
            var typeEntity = entity.GetType();

            bool isIEntity = true;
            if (entity.State == EntityState.Detached || entity.State == EntityState.Unchanged )
                continue;

            if (isIEntity)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        var addedEntity = entity as ICreationAudit;
                        addedEntity.CreationTime = DateTime.Now;
                        addedEntity.CreatorId = _session.UserId;
                        break;
                    case EntityState.Modified:
                        var deletedEntity = entity as IDeletionAudit;
                        if (deletedEntity.IsDeleted)
                        {
                            deletedEntity.DeletionTime = DateTime.Now;
                            deletedEntity.DeletorId = _session.UserId;
                        }
                        else
                        {
                            var modifiedEntity = entity as IModificationAudit;
                            modifiedEntity.ModificationTime = DateTime.Now;
                            modifiedEntity.ModifiorId = _session.UserId;
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

    private readonly IEcommerceSession _session;
    #endregion
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public eCommerceDbContext(DbContextOptions options, IEcommerceSession session) : base(options)
    {
        _session = session;
    }
}