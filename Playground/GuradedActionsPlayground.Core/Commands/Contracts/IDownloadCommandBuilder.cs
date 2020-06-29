using GuardedActions.Commands.Contracts;
using GuardedActionsPlayground.Core.Models;

namespace GuardedActionsPlayground.Core.Commands.Contracts
{
    public interface IDownloadCommandBuilder : IAsyncGuardedDataContextCommandBuilder<Download>
    {
    }
}
