using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuardedActions.Commands.Actions;
using XamarinFormsMvvmCrossSample.Core.Commands.Actions.Contracts;
using XamarinFormsMvvmCrossSample.Core.Factories.Contracts;
using XamarinFormsMvvmCrossSample.Core.Models;
using XamarinFormsMvvmCrossSample.Core.ViewModels;

namespace XamarinFormsMvvmCrossSample.Core.Commands.Actions
{
    public class PullDownloadListAction : GuardedDataContextAction<MainViewModel>, IPullDownloadListAction
    {
        private readonly IDownloadFactory _downloadFactory;

        public PullDownloadListAction(IDownloadFactory downloadFactory)
        {
            _downloadFactory = downloadFactory ?? throw new ArgumentNullException(nameof(downloadFactory));
        }

        protected override Task Execute()
        {
            DataContext.Downloads = new List<Download>
            {
                _downloadFactory.Create("https://www.google.com/"),
                _downloadFactory.Create("https://www.facebook.com/"),
                _downloadFactory.Create("This is not an URL")
            };
            return Task.CompletedTask;
        }
    }
}
