using GuardedActionsSample.Commands.Contracts;
using GuardedActionsSample.Factories.Contracts;
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
