using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplicationBoilerplate.Infrastucture
{
    public interface IStartup
    {
        void ConfigureAutofac(ContainerBuilder containerBuilder);
        void ConfigureServices(IServiceCollection services);
    }
}