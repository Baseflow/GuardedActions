using GuardedActionsSample.Core.Commands;
using GuardedActionsSample.Core.Commands.Actions;
using GuardedActionsSample.Core.Factories.Contracts;
using GuardedActionsSample.Core.Models;

namespace GuardedActionsSample.Core.Factories
{
    public class DownloadFactory : IDownloadFactory
    {
        public Download Create(string url)
        {
            var action = new DownloadUrlAction();
            var commandBuilder = new DownloadCommandBuilder(action);
            var download = new Download(url, commandBuilder);
            return download;
        }
    }
}
