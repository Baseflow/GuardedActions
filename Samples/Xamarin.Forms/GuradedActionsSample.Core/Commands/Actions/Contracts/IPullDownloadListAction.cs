using GuardedActions.Commands.Actions.Contracts;
using GuardedActionsSample.Core.ViewModels;

namespace GuardedActionsSample.Core.Commands.Actions.Contracts
{
    public interface IPullDownloadListAction : IGuardedDataContextAction<MainViewModel>
    {
    }
}
