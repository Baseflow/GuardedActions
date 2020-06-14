using GuardedActions.Interfaces;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Actions.Main.Interfaces
{
    public interface IPullDownloadListAction : IGuardedDataContextAction<MainViewModel>
    {
    }
}
