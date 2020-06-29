using GuardedActionsPlayground.Core.Commands;
using GuardedActionsPlayground.Core.Commands.Actions;
using GuardedActionsPlayground.Core.Factories.Contracts;
using GuardedActionsPlayground.Core.Models;

namespace GuardedActionsPlayground.Core.Factories
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
