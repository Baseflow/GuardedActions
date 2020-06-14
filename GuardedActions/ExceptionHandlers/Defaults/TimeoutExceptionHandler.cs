using System;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Interfaces;

namespace GuardedActions.ExceptionHandlers.Defaults
{
    [DefaultExceptionHandler]
    public class TimeoutExceptionHandler : ExceptionHandler<TimeoutException>
    {
        public override Task Handle(IExceptionHandlingAction<TimeoutException> exceptionHandlingAction)
        {
            //ignore
            exceptionHandlingAction.HandlingShouldFinish = true;
            return Task.CompletedTask;
        }
    }
}
