using System;

namespace GuardedActions.ExceptionHandlers.Contracts
{
    public interface IExceptionHandlingAction
    {
        bool HandlingShouldFinish { get; set; }
    }

    public interface IExceptionHandlingAction<out TException> : IExceptionHandlingAction where TException : Exception
    {
        TException Exception { get; }
    }
}
