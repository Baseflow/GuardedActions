using GuardedActions.Commands.Contracts;
using XamarinFormsMvvmCrossSample.Core.Models;

namespace XamarinFormsMvvmCrossSample.Core.Commands.Contracts
{
    public interface IDownloadCommandBuilder : IAsyncGuardedDataContextCommandBuilder<Download>
    {
    }
}
