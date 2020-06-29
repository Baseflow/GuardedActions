using GuardedActions.Commands.Contracts;
using GuardedActionsPlayground.Core.ViewModels;

namespace GuardedActionsPlayground.Core.Commands.Contracts
{
    public interface IInitializeCommandBuilder : IAsyncGuardedDataContextCommandBuilder<MainViewModel>
    {
    }
}
