using System.Reflection;
using eCommerce.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eCommerce.EntityFrameworkCore;
/// <summary>
/// This is a convention for add migration when use cmd
/// </summary>
public class eCommerceDesignTimeContextFactory : IDesignTimeDbContextFactory<eCommerceDbContext>
{
    public eCommerceDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<eCommerceDbContext>();

        var currentLocation = Assembly.GetExecutingAssembly().Location;
        var rootPathMachine = currentLocation.Split("eCommerce");
        var basePathConfig = Path.Combine(rootPathMachine[0], "eCommerce", "eCommerce.Host");
        var configuration = LocalConfigurationExtentions.GetConfigurationBuilder(basePathConfig);
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

        return new eCommerceDbContext(optionsBuilder.Options);
    }
}