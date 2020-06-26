using System.Net;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;
using GuardedActionsSample.Core.Commands.Actions.Contracts;
using GuardedActionsSample.Core.Models;

namespace GuardedActionsSample.Core.Commands.Actions.ExceptionHandlers
{
    [ExceptionHandlerFor(typeof(IDownloadUrlAction))]
    public class DownloadUrlActionWebExceptionHandler : ContextExceptionHandler<WebException, Download>
    {
        public override Task Handle(IExceptionHandlingAction<WebException, Download> exceptionHandlingAction)
        {
            if (exceptionHandlingAction?.DataContext != null)
            {
                exceptionHandlingAction.DataContext.ErrorMessage = exceptionHandlingAction?.Exception?.Message;
            }

            return Task.CompletedTask;
        }
    }
}
