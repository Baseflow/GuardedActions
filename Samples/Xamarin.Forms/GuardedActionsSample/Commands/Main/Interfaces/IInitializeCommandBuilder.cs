using GuardedActions.Commands.Interfaces;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Main.Interfaces
{
    public interface IInitializeCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
