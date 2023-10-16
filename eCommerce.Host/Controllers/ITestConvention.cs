using eCommerce.Shared.Cores.DependencyInjections;

namespace eCommerce.Host.Controllers;

public interface ITestConvention : ITransientDependency
{
    string GetName();
}