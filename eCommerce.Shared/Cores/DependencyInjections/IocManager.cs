using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace eCommerce.Shared.Cores.DependencyInjections;

public class IocManager : IIocManager
{
    public IWindsorContainer Instance { get => _container;
        set => _container = value;
    }
    private IWindsorContainer _container;
    
    public T Resolve<T>() => _container.Resolve<T>();
    public T Resolve<T>(Type type) => (T)Instance.Resolve(type);
    public void RegisterByConventional(IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            _container.Register(Classes.FromAssembly(assembly));
        }
    }
}