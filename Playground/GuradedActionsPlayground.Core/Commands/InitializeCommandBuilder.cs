using System;
using System.Threading.Tasks;
using GuardedActions.Commands;
using GuardedActions.Commands.Contracts;
using GuardedActionsPlayground.Core.Commands.Actions.Contracts;
using GuardedActionsPlayground.Core.Commands.Contracts;
using GuardedActionsPlayground.Core.ViewModels;

namespace GuardedActionsPlayground.Core.Commands
{
    public class InitializeCommandBuilder : AsyncGuardedDataContextCommandBuilder<MainViewModel>, IInitializeCommandBuilder
    {
        private readonly IPullDownloadListAction _pullDownloadListAction;

        public InitializeCommandBuilder(IPullDownloadListAction pullDownloadListAction)
        {
            _pullDownloadListAction = pullDownloadListAction ?? throw new ArgumentNullException(nameof(pullDownloadListAction));
        }

        protected override Task ExecuteCommandAction()
        {
            _pullDownloadListAction.ExecuteGuarded();

            // And more actions to initialize

            return Task.CompletedTask;
        }

        public override IAsyncGuardedDataContextCommandBuilder<MainViewModel> RegisterDataContext(MainViewModel dataContext)
        {
            _pullDownloadListAction.RegisterDataContext(dataContext);

            return base.RegisterDataContext(dataContext);
        }
    }
}
