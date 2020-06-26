using System;
using System.Threading.Tasks;
using GuardedActions.Commands;
using GuardedActions.Commands.Contracts;
using GuardedActionsSample.Core.Commands.Actions.Contracts;
using GuardedActionsSample.Core.Commands.Contracts;
using GuardedActionsSample.Core.Models;

namespace GuardedActionsSample.Core.Commands
{
    public class DownloadCommandBuilder : AsyncGuardedDataContextCommandBuilder<Download>, IDownloadCommandBuilder
    {
        private readonly IDownloadUrlAction _downloadUrlAction;

        public DownloadCommandBuilder(IDownloadUrlAction downloadUrlAction)
        {
            _downloadUrlAction = downloadUrlAction ?? throw new ArgumentNullException(nameof(downloadUrlAction));
        }

        protected override Task ExecuteCommandAction()
        {
            _downloadUrlAction.ExecuteGuarded();

            // And more actions to handle the download

            return Task.CompletedTask;
        }

        public override IAsyncGuardedDataContextCommandBuilder<Download> RegisterDataContext(Download dataContext)
        {
            _downloadUrlAction.RegisterDataContext(dataContext);

            return base.RegisterDataContext(dataContext);
        }
    }
}
