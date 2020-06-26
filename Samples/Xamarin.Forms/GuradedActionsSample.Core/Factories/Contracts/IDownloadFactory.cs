using GuardedActionsSample.Core.Commands.Contracts;
using GuardedActionsSample.Core.Models;

namespace GuardedActionsSample.Core.Factories.Contracts
{
    public interface IDownloadFactory
    {
        Download Create(string url);
    }
}
