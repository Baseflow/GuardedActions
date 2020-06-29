using System.Net;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;
using GuardedActionsPlayground.Core.Commands.Actions.Contracts;
using GuardedActionsPlayground.Core.Models;

namespace GuardedActionsPlayground.Core.Commands.Actions.ExceptionHandlers
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
