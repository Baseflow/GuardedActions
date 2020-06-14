using GuardedActions.Interfaces;
using GuardedActionsSample.Models;

namespace GuardedActionsSample.Actions.Main.Interfaces
{
    public interface IDownloadUrlAction : IGuardedDataContextAction<Download>
    {
    }
}
