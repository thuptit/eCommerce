using eCommerce.Domain.Domains.Users;
using eCommerce.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Npgsql.EntityFrameworkCore;

namespace eCommerce.Domain;

[DependsOn(typeof(eCommerceEntityFrameworkCoreModule))]
public class eCommerceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(UserDomain));
        context.Services.AddTransient(typeof(SignInDomain));
        context.Services.AddTransient(typeof(RoleDomain));
    }
}