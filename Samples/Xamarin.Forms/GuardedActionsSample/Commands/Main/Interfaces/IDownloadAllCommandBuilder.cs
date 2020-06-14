using GuardedActions.Commands.Interfaces;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Main.Interfaces
{
    public interface IDownloadAllCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
