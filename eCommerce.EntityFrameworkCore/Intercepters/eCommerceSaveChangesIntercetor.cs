using eCommerce.EntityFrameworkCore.Audits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace eCommerce.EntityFrameworkCore.Intercepters;

public class eCommerceSaveChangesIntercetor : SaveChangesInterceptor
{
    private readonly long? _userId;
    private readonly ILogger<eCommerceDbContext> _logger;
    public eCommerceSaveChangesIntercetor(long? userId, ILogger<eCommerceDbContext> logger)
    {
        _userId = userId;
        _logger = logger;
    }
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        ApplyAudits(eventData.Context?.ChangeTracker);
        return base.SavedChanges(eventData, result);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudits(eventData.Context?.ChangeTracker);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyAudits(eventData.Context?.ChangeTracker);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    private void ApplyAudits(ChangeTracker? changeTracker)
    {
        if (changeTracker == null)
            return;
        changeTracker.DetectChanges();
        foreach (var entry in changeTracker.Entries())
        {
            try
            {
                var entity = (IEntity?)entry.Entity;
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entity == null)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreationTime = DateTime.Now;
                        entity.CreatorId = _userId;
                        break;
                    case EntityState.Modified:
                        if (entity.IsDeleted)
                        {
                            entity.DeletionTime = DateTime.Now;
                            entity.DeletorId = _userId;
                        }
                        else
                        {
                            entity.ModificationTime = DateTime.Now;
                            entity.ModifiorId = _userId;
                        }
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}