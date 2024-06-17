using NetApi.Interfaces;

namespace NetApi.Services
{
    public class TestService : ITestService
    {
        public string Get()
        {
           return "Hello World!, Service Injected";
        }
    }
}