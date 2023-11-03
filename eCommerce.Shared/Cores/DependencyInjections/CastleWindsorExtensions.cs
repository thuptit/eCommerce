using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Shared.Cores.DependencyInjections;

public static class CastleWindsorExtensions
{
    public static void AddWindsorCastle(this WindsorContainer container)
    {
        string searchPattern = "eCommerce.*.dll";
        var path = AppDomain.CurrentDomain.BaseDirectory;
        var assemblies = Directory.GetFiles(path, searchPattern).Select(Assembly.LoadFrom);
        foreach (var assembly in assemblies)
        {
            container.Register(Classes.FromAssembly(assembly)
                .IncludeNonPublicTypes()
                .BasedOn<ITransientDependency>()
                .WithService.Self()
                .WithService.DefaultInterfaces()
                .LifestyleTransient());
        
            container.Register(Classes.FromAssembly(assembly)
                .IncludeNonPublicTypes()
                .BasedOn<ISingletonDependency>()
                .WithService.DefaultInterfaces()
                .WithService.Self()
                .LifestyleSingleton());
        }
    }
}