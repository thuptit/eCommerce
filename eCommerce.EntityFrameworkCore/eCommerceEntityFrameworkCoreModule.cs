using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.EntityFrameworkCore.Seeds;
using eCommerce.EntityFrameworkCore.UnitOfWorks;
using eCommerce.Shared;
using eCommerce.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace eCommerce.EntityFrameworkCore;

[DependsOn(typeof(eCommerceSharedModule))]
public class eCommerceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = LocalConfigurationExtentions.GetConfigurationBuilder();
        context.Services.AddDbContext<eCommerceDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        context.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<eCommerceDbContext>()
            .AddDefaultTokenProviders();
        context.Services.AddScoped<IUnitOfWork, eCommerceDbContext>();
        context.Services.AddHostedService<SeedDataService>();
        context.Services.AddScoped(typeof(UnitOfWorkMiddleware));
    }
}