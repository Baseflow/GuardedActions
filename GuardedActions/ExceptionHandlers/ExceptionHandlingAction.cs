using System;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers
{
    public class ExceptionHandlingAction<TException> : IExceptionHandlingAction<TException>
        where TException : Exception
    {
        public ExceptionHandlingAction(TException exception)
        {
            Exception = exception;
        }

        public bool HandlingShouldFinish { get; set; }

        public TException Exception { get; }
    }

    public class ExceptionHandlingAction<TException, TDataContext> : IExceptionHandlingAction<TException, TDataContext>
        where TException : Exception
        where TDataContext : class
    {
        public ExceptionHandlingAction(TException exception, TDataContext dataContext)
        {
            Exception = exception;
            DataContext = dataContext;
        }

        public bool HandlingShouldFinish { get; set; }

        public new TException Exception { get; }

        public TDataContext DataContext { get; }
    }
}
