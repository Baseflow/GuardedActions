using GuardedActions.Commands.Actions.Contracts;
using GuardedActionsPlayground.Core.Models;

namespace GuardedActionsPlayground.Core.Commands.Actions.Contracts
{
    public interface IDownloadUrlAction : IGuardedDataContextAction<Download>
    {
    }
}
