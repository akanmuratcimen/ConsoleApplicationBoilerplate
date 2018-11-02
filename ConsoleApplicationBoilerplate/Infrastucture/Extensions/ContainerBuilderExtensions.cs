using System.Reflection;
using Autofac;

namespace ConsoleApplicationBoilerplate.Infrastucture.Extensions
{
    internal static class ContainerBuilderExtensions
    {
        public static void RegisterDependencies(this ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(t => typeof(ITransient).IsAssignableFrom(t))
                .InstancePerDependency().AsImplementedInterfaces();

            containerBuilder
                .RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(t => typeof(ISingleton).IsAssignableFrom(t))
                .SingleInstance().AsImplementedInterfaces();
        }
    }
}