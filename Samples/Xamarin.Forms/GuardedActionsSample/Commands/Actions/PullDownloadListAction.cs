using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuardedActions.Commands.Actions;
using GuardedActionsSample.Commands.Actions.Contracts;
using GuardedActionsSample.Factories.Contracts;
using GuardedActionsSample.Models;
using GuardedActionsSample.ViewModels;

namespace GuardedActionsSample.Commands.Actions
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
