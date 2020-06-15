using GuardedActionsSample.Models;

namespace GuardedActionsSample.Factories.Contracts
{
    public interface IDownloadFactory
    {
        Download Create(string url);
    }
}
