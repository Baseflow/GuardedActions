using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActionsSample.ExceptionHandlers.Defaults
{
    [DefaultExceptionHandler]
    public class TaskCanceledExceptionHandler : ExceptionHandler<TaskCanceledException>
    {
        public override Task Handle(IExceptionHandlingAction<TaskCanceledException> exceptionHandlingAction) => Task.CompletedTask; //ignore
    }
}
