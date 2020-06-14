using GuardedActionsSample.Actions.Main.Interfaces;
using GuardedActionsSample.Factories.Interfaces;
using GuardedActionsSample.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActionsSample.Factories
{
    public class DownloadFactory : IDownloadFactory
    {
        public Download Create(string url)
        {
            var downloadUrlAction = Startup.Services.GetService<IDownloadUrlAction>();
            var download = new Download(url, downloadUrlAction);
            downloadUrlAction.RegisterDataContext(download);
            return download;
        }
    }
}
