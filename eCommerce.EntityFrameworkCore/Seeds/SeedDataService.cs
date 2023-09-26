using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace eCommerce.EntityFrameworkCore.Seeds;

public class SeedDataService : IHostedService
{
    private readonly eCommerceDbContext _dbContext;

    public SeedDataService(eCommerceDbContext context)
    {
        _dbContext = context;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Database.MigrateAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}