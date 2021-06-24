using Chromely;
using Microsoft.Extensions.DependencyInjection;

namespace ChromelyBrowser
{
    public class ChromelyApplication : ChromelyBasicApp
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            RegisterControllerAssembly(services, typeof(ChromelyApplication).Assembly);
        }
    }
}