using GuardedActions.Commands.Interfaces;
using GuardedActionsSample.Models;

namespace GuardedActionsSample.Commands.Main.Interfaces
{
    public interface IDownloadCommandBuilder : IAsyncGuardedDataContextCommandBuilder<Download>
    {
    }
}
