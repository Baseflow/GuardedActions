using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers.Defaults
{
    [DefaultExceptionHandler]
    public class CommonExceptionHandler : ExceptionHandler<Exception>
    {
        public override Task Handle(IExceptionHandlingAction<Exception> exceptionHandlingAction)
        {
            //TODO handle, display dialog, send to appcenter etc
            //TODO Jop: Maybe also generate a code to show in the dialog when QA criteria met so they can report the code and the dev can trace it's code easily in App Center.
            Debug.WriteLine(exceptionHandlingAction?.Exception);
            exceptionHandlingAction.HandlingShouldFinish = true;
            return Task.CompletedTask;
        }
    }
}
