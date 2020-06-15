using GuardedActions.Commands.Actions.Contracts;
using GuardedActionsSample.Models;

namespace GuardedActionsSample.Commands.Actions.Contracts
{
    public interface IDownloadUrlAction : IGuardedDataContextAction<Download>
    {
    }
}
