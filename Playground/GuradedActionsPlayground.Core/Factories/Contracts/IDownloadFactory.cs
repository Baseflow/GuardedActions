using GuardedActionsPlayground.Core.Commands.Contracts;
using GuardedActionsPlayground.Core.Models;

namespace GuardedActionsPlayground.Core.Factories.Contracts
{
    public interface IDownloadFactory
    {
        Download Create(string url);
    }
}
