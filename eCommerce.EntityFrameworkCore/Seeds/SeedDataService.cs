using eCommerce.EntityFrameworkCore.Entities;
using Microsoft.AspNetCore.Identity;
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
            var _userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            await _dbContext.Database.MigrateAsync();
            var seed = new SeedData(_dbContext, _userManager);
            await seed.UserRoleSeed();
        }
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}