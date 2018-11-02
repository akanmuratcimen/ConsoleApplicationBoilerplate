using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConsoleApplicationBoilerplate.Infrastucture.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplicationBoilerplate.Infrastucture
{
    public class ApplicationHost<TStartup> where TStartup : IStartup, new()
    {
        private static MethodInfo GetMethod<T>(string methodName)
        {
            return typeof(T).GetTypeInfo().GetDeclaredMethod(methodName);
        }

        public static Task Run<TApplication>() where TApplication : class
        {
            return Run<TApplication>(new string[] { });
        }

        public static async Task Run<TApplication>(string[] args) where TApplication : class
        {
            var applicationRunMethod = GetMethod<TApplication>("Run");

            if (applicationRunMethod == null)
            {
                throw new ApplicationException($"The '{typeof(TApplication).Name}' " +
                    "class must have a public void method named 'Run'.");
            }

            var services = new ServiceCollection();

            services.AddLogging();
            services.AddOptions();

            services.AddTransient<TApplication>();

            var startup = new TStartup();
            var containerBuilder = new ContainerBuilder();

            startup.ConfigureServices(services);
            startup.ConfigureAutofac(containerBuilder);

            containerBuilder.Populate(services);
            containerBuilder.RegisterDependencies();

            var container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);

            var configureMethod = GetMethod<TStartup>("Configure");

            if (configureMethod != null)
            {
                var configureMethodParameters = configureMethod.GetParameters()
                    .Select(x => serviceProvider.GetService(x.ParameterType))
                    .ToArray();

                configureMethod.Invoke(startup, configureMethodParameters);
            }

            await (Task)applicationRunMethod.Invoke(
                serviceProvider.GetService<TApplication>(),
                new object[] { args });
        }
    }
}