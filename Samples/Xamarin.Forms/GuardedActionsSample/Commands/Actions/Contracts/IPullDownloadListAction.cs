using GuardedActions.Commands.Actions.Contracts;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Actions.Contracts
{
    public interface IPullDownloadListAction : IGuardedDataContextAction<MainViewModel>
    {
    }
}
