using GuardedActions.Commands.Actions.Contracts;
using GuardedActionsSample.Core.Models;

namespace GuardedActionsSample.Core.Commands.Actions.Contracts
{
    public interface IDownloadUrlAction : IGuardedDataContextAction<Download>
    {
    }
}
