using GuardedActionsSample.Actions.Main.Interfaces;
using GuardedActionsSample.Commands.Main.Interfaces;
using GuardedActionsSample.Factories.Interfaces;
using GuardedActionsSample.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActionsSample.Factories
{
    public class DownloadFactory : IDownloadFactory
    {
        public Download Create(string url)
        {
            var downloadCommandBuilder = Startup.Services.GetService<IDownloadCommandBuilder>();
            var download = new Download(url, downloadCommandBuilder);
            return download;
        }
    }
}
