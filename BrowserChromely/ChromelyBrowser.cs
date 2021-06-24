using Chromely.Core;

namespace ChromelyBrowser
{
    public class ChromelyBrowser
    {
        public ChromelyBrowser(string[] args)
        {
            AppBuilder.Create().UseApp<ChromelyApplication>().Build().Run(args);
        }
    }
}