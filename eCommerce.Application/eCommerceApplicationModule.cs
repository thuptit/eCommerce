using System.Reflection;
using eCommerce.Application.Validators;
using eCommerce.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using FluentValidation;

namespace eCommerce.Application;

[DependsOn(typeof(eCommerceDomainModule))]
public class eCommerceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        context.Services.AddScoped<IApplicationService, ApplicationServiceBase>();
        context.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        context.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}