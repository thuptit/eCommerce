using System.Net;
using eCommerce.Shared.Cores.Caches;
using eCommerce.Shared.Cores.DependencyInjections;
using eCommerce.Shared.Cores.Responses;
using eCommerce.Shared.Cores.Sessions;
using eCommerce.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.Modularity;

namespace eCommerce.Shared;

public class eCommerceSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        #region DI
        context.Services.AddScoped(typeof(WrapperResponseMiddleware));
        context.Services.AddScoped<IEcommerceSession, EcommerceSession>();
        context.Services.AddScoped<ICacheService, CacheService>();
        #endregion
        var configuration = LocalConfigurationExtentions.GetConfigurationBuilder();;
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
        context.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["RedisCache"];
            options.InstanceName = "eCommerce";
        });
        
    }
}