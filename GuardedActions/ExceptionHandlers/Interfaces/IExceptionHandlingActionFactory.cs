using System;

namespace GuardedActions.ExceptionHandlers.Interfaces
{
    public interface IExceptionHandlingActionFactory
    {
        IExceptionHandlingAction Create(Exception exception);
    }
}
