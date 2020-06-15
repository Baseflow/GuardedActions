using GuardedActions.Commands.Contracts;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Contracts
{
    public interface IDownloadAllCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
