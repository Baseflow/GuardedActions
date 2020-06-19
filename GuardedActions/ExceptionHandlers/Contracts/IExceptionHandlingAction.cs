using System;
using GuardedActions.Contracts;

namespace GuardedActions.ExceptionHandlers.Contracts
{
    public interface IExceptionHandlingAction
    {
        bool HandlingShouldFinish { get; set; }
    }

    public interface IExceptionHandlingAction<out TException> : IExceptionHandlingAction
        where TException : Exception
    {
        TException Exception { get; }
    }

    public interface IExceptionHandlingAction<out TException, out TDataContext> : IExceptionHandlingAction, IDataContext<TDataContext>
        where TException : Exception
        where TDataContext : class
    {
        TException Exception { get; }
    }
}
