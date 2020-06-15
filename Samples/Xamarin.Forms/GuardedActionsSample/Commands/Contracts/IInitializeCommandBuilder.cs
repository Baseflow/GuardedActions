using GuardedActions.Commands.Contracts;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Contracts
{
    public interface IInitializeCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
