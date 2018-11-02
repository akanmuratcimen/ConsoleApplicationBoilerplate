using System.Threading.Tasks;
using Autofac;
using ConsoleApplicationBoilerplate.Infrastucture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApplicationBoilerplate
{
    internal class Startup : IStartup
    {
        internal void Configure(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
        }

        public void ConfigureAutofac(ContainerBuilder containerBuilder)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        internal static async Task Main(string[] args)
        {
            await ApplicationHost<Startup>.Run<Application>(args);
        }
    }
}