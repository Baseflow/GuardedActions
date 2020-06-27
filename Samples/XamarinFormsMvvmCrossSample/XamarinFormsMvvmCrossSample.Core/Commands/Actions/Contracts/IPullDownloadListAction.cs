using GuardedActions.Commands.Actions.Contracts;
using XamarinFormsMvvmCrossSample.Core.ViewModels;

namespace XamarinFormsMvvmCrossSample.Core.Commands.Actions.Contracts
{
    public interface IPullDownloadListAction : IGuardedDataContextAction<MainViewModel>
    {
    }
}
