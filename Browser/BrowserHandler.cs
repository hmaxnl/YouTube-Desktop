using System.Threading.Tasks;
using Chromely;
using Chromely.Core;
using Chromely.Core.Configuration;
using Chromely.Core.Infrastructure;
using Chromely.Core.Logging;
using Microsoft.Extensions.Configuration;

namespace Browser
{
    public class BrowserHandler
    {
        private BrowserWindow _browserWindow;
        private AppBuilder _appBuilder;
        public BrowserHandler(string[] args)
        {
            _appBuilder = AppBuilder.Create().UseApp<BrowserApp>().Build();
            Task runTask = new Task(() => { _appBuilder.Run(args); });
            runTask.Start();
        }
    }
}