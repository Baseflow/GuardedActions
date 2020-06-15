using System;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers
{
    public class ExceptionHandlingAction<TException> : IExceptionHandlingAction<TException> where TException : Exception
    {
        public ExceptionHandlingAction(TException exception)
        {
            Exception = exception;
        }

        public bool HandlingShouldFinish { get; set; }

        public TException Exception { get; }
    }
}
