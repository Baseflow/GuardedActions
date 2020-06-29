using System;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActionsPlayground.Core.ExceptionHandlers.Defaults
{
    [DefaultExceptionHandler]
    public class GeneralExceptionHandler : GuardedActions.ExceptionHandlers.Defaults.GeneralExceptionHandler
    {
        public override Task Handle(IExceptionHandlingAction<Exception> exceptionHandlingAction)
        {
#if DEBUG
            Console.WriteLine("Custom general exception hanlder!");
#endif
            return base.Handle(exceptionHandlingAction);
        }
    }
}
