using eCommerce.Domain.Domains;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace eCommerce.Domain;

[DependsOn(typeof(eCommerceEntityFrameworkCoreModule))]
public class eCommerceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(UserDomain));
        context.Services.AddTransient(typeof(SignInDomain));
        context.Services.AddTransient(typeof(RoleDomain));
        context.Services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
    }
}