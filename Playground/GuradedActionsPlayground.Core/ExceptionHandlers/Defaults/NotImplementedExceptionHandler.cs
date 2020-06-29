using System;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActionsPlayground.Core.ExceptionHandlers.Defaults
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
            return Task.CompletedTask;
        }
    }
}
