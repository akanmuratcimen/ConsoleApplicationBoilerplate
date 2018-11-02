using System;
using System.Threading.Tasks;
using ConsoleApplicationBoilerplate.Services;

namespace ConsoleApplicationBoilerplate
{
    public class Application
    {
        private readonly IDummyTransientService _dummyTransientService;

        public Application(IDummyTransientService dummyTransientService)
        {
            _dummyTransientService = dummyTransientService;
        }

        public async Task Run(string[] args)
        {
            Console.WriteLine(_dummyTransientService.DummyMethod());

            await Task.CompletedTask;
        }
    }
}