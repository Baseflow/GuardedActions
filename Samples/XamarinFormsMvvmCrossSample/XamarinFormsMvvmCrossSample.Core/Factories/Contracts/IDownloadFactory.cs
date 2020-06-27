using XamarinFormsMvvmCrossSample.Core.Commands.Contracts;
using XamarinFormsMvvmCrossSample.Core.Models;

namespace XamarinFormsMvvmCrossSample.Core.Factories.Contracts
{
    public interface IDownloadFactory
    {
        Download Create(string url);
    }
}
