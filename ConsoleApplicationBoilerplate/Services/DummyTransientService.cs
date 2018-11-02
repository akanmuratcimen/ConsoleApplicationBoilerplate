using ConsoleApplicationBoilerplate.Infrastucture;

namespace ConsoleApplicationBoilerplate.Services
{
    public interface IDummyTransientService : ITransient
    {
        string DummyMethod();
    }

    public class DummyTransientService : IDummyTransientService
    {
        public string DummyMethod()
        {
            return "DummyTransientService.DummyMethod() called...";
        }
    }
}