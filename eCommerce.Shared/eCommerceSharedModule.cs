using eCommerce.Shared.Cores.Responses;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace eCommerce.Shared;

public class eCommerceSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddScoped(typeof(WrapperResponseMiddleware));
    }
}