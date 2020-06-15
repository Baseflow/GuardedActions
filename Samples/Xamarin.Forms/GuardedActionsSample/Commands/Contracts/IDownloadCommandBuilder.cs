using GuardedActions.Commands.Contracts;
using GuardedActionsSample.Models;

namespace GuardedActionsSample.Commands.Contracts
{
    public interface IDownloadCommandBuilder : IAsyncGuardedDataContextCommandBuilder<Download>
    {
    }
}
