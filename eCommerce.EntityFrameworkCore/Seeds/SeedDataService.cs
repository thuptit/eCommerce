using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eCommerce.EntityFrameworkCore.Seeds;

public class SeedDataService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SeedDataService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var _dbContext = scope.ServiceProvider.GetService<eCommerceDbContext>();
            await _dbContext.Database.MigrateAsync();
            var seed = new SeedData(_dbContext);
            await seed.UserRoleSeed();
        }
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}