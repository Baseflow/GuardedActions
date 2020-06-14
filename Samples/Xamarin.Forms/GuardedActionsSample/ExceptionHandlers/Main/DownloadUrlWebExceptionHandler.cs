using System.Net;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Interfaces;
using GuardedActionsSample.Actions.Main.Interfaces;
using GuardedActionsSample.Models;

namespace GuardedActionsSample.ErrorHandlers.FileManagement
{
    [ExceptionHandlerFor(typeof(IDownloadUrlAction))]
    public class DownloadUrlWebExceptionHandler : ContextExceptionHandler<WebException, Download>
    {
        public override Task Handle(IExceptionHandlingAction<WebException> exceptionHandlingAction)
        {
            if (Context.TryGetTarget(out Download download))
            {
                download.ErrorMessage = exceptionHandlingAction?.Exception?.Message;
            }
            return Task.CompletedTask;
        }
    }
}
