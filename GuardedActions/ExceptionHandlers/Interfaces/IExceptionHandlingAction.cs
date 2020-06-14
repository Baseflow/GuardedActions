using System;

namespace GuardedActions.ExceptionHandlers.Interfaces
{
    public interface IExceptionHandlingAction<out TException> : IExceptionHandlingAction where TException : Exception
    {
        TException Exception { get; }
    }
}
