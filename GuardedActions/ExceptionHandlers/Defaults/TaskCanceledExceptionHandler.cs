using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Interfaces;

namespace GuardedActions.ExceptionHandlers.Defaults
{
    [DefaultExceptionHandler]
    public class TaskCanceledExceptionHandler : ExceptionHandler<TaskCanceledException>
    {
        public override Task Handle(IExceptionHandlingAction<TaskCanceledException> exceptionHandlingAction)
        {
            //ignore
            exceptionHandlingAction.HandlingShouldFinish = true;
            return Task.CompletedTask;
        }
    }
}
