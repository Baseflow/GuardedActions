using System;
using System.Net;
using System.Threading.Tasks;
using GuardedActions.Commands.Actions;
using GuardedActionsSample.Commands.Actions.Contracts;
using GuardedActionsSample.Models;

namespace GuardedActionsSample.Commands.Actions
{
    public class DownloadUrlAction : GuardedDataContextAction<Download>, IDownloadUrlAction
    {
        protected override Task Execute()
        {
            var correctUrl = Uri.TryCreate(DataContext.Url, UriKind.Absolute, out var result);

            if (!correctUrl)
                throw new WebException("Manually triggered");

            DataContext.IsDownloaded = true;

            return Task.CompletedTask;
        }
    }
}
