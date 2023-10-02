using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eCommerce.EntityFrameworkCore.Intercepters;

public class eCommerceSaveChangesIntercetor : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}