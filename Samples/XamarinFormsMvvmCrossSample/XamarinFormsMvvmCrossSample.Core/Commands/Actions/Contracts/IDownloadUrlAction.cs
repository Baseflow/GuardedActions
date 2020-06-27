using GuardedActions.Commands.Actions.Contracts;
using XamarinFormsMvvmCrossSample.Core.Models;

namespace XamarinFormsMvvmCrossSample.Core.Commands.Actions.Contracts
{
    public interface IDownloadUrlAction : IGuardedDataContextAction<Download>
    {
    }
}
