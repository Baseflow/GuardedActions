using GuardedActions.Commands.Contracts;
using GuardedActionsSample.Core.Models;

namespace GuardedActionsSample.Core.Commands.Contracts
{
    public interface IDownloadCommandBuilder : IAsyncGuardedDataContextCommandBuilder<Download>
    {
    }
}
