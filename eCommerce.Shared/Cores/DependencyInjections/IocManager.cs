using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace eCommerce.Shared.Cores.DependencyInjections;

public class IocManager : IIocManager
{
    // private static IWindsorContainer _container = new WindsorContainer();
    // public IWindsorContainer Instance => _container;
    //
    // public T Resolve<T>() => Instance.Resolve<T>();
    // public object Resolve(Type type) => Instance.Resolve(type);
    // public void RegisterByConventional(IEnumerable<Assembly> assemblies)
    // {
    //     foreach (var assembly in assemblies)
    //     {
    //         Instance.Register(Classes.FromAssembly(assembly)
    //             .IncludeNonPublicTypes()
    //             .BasedOn<ITransientDependency>()
    //             .WithService.Self()
    //             .WithService.DefaultInterfaces()
    //             .LifestyleTransient());
    //
    //         Instance.Register(Classes.FromAssembly(assembly)
    //             .IncludeNonPublicTypes()
    //             .BasedOn<ISingletonDependency>()
    //             .WithService.DefaultInterfaces()
    //             .WithService.Self()
    //             .LifestyleSingleton());
    //     }
    // }
}