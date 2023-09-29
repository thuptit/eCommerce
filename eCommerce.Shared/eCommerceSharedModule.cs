using eCommerce.Shared.Cores.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace eCommerce.Shared;

public class eCommerceSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddScoped(typeof(WrapperResponseMiddleware));
        context.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireClaim("Role", eCommerceConsts.RoleAdmin);
            });
            options.AddPolicy("Staff", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireClaim("Role", eCommerceConsts.RoleStaff);
            });
        });
    }
}