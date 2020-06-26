using GuardedActions.Commands.Contracts;
using GuardedActionsSample.Core.ViewModels;

namespace GuardedActionsSample.Core.Commands.Contracts
{
    public interface IInitializeCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
