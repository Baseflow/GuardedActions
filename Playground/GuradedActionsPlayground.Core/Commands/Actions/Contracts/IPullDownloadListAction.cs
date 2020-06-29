using GuardedActions.Commands.Actions.Contracts;
using GuardedActionsPlayground.Core.ViewModels;

namespace GuardedActionsPlayground.Core.Commands.Actions.Contracts
{
    public interface IPullDownloadListAction : IGuardedDataContextAction<MainViewModel>
    {
    }
}
