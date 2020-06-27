using XamarinFormsMvvmCrossSample.Core.Commands;
using XamarinFormsMvvmCrossSample.Core.Commands.Actions;
using XamarinFormsMvvmCrossSample.Core.Factories.Contracts;
using XamarinFormsMvvmCrossSample.Core.Models;

namespace XamarinFormsMvvmCrossSample.Core.Factories
{
    public class DownloadFactory : IDownloadFactory
    {
        public Download Create(string url)
        {
            var action = new DownloadUrlAction();
            var commandBuilder = new DownloadCommandBuilder(action);
            var download = new Download(url, commandBuilder);
            return download;
        }
    }
}
