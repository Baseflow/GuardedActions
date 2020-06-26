using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuardedActions.Commands.Actions;
using GuardedActionsSample.Core.Commands.Actions.Contracts;
using GuardedActionsSample.Core.Factories.Contracts;
using GuardedActionsSample.Core.Models;
using GuardedActionsSample.Core.ViewModels;

namespace GuardedActionsSample.Core.Commands.Actions
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
