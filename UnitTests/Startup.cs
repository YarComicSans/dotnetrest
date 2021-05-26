using API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Rest.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDeadlockParser, DeadlockParser>();
        }
    }
}
