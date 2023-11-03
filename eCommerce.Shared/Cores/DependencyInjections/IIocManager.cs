using System.Reflection;

namespace eCommerce.Shared.Cores.DependencyInjections;

public interface IIocManager
{
    T Resolve<T>();
    T Resolve<T>(Type type);
    void RegisterByConventional(IEnumerable<Assembly> assemblies);
}