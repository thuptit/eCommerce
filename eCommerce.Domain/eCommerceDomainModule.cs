using eCommerce.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Npgsql.EntityFrameworkCore;

namespace eCommerce.Domain;

[DependsOn(typeof(eCommerceEntityFrameworkCoreModule))]
public class eCommerceDomainModule : AbpModule
{
}