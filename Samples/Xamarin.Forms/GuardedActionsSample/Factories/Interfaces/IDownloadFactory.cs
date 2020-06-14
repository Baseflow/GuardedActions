using GuardedActionsSample.Models;

namespace GuardedActionsSample.Factories.Interfaces
{
    public interface IDownloadFactory
    {
        Download Create(string url);
    }
}
