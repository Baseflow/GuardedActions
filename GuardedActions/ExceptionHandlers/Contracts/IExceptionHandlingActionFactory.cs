using System;

namespace GuardedActions.ExceptionHandlers.Contracts
{
    public interface IExceptionHandlingActionFactory
    {
        IExceptionHandlingAction Create(Exception exception, object context = null);
    }
}
