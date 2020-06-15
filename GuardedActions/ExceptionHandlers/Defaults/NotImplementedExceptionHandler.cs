using System;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers.Defaults
{
    //TODO Think of a good implementation.
    [DefaultExceptionHandler]
    public class NotImplementedExceptionHandler : ExceptionHandler<NotImplementedException>
    {
        //private readonly IUserDialogs _userDialogs;

        //public NotImplementedExceptionHandler(IUserDialogs userDialogs)
        public NotImplementedExceptionHandler()
        {
            //_userDialogs = userDialogs;
        }

        public override Task Handle(IExceptionHandlingAction<NotImplementedException> exceptionHandlingAction)
        {
            //await _userDialogs.AlertAsync("This is not implemented... yet.", "Coming soon™", "Ok, I will wait");
            exceptionHandlingAction.HandlingShouldFinish = true;
            return Task.CompletedTask;
        }
    }
}
