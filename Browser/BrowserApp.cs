using Chromely;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Browser
{
    public class BrowserApp : ChromelyBasicApp
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            
            RegisterControllerAssembly(services, typeof(BrowserApp).Assembly);
        }
    }
}