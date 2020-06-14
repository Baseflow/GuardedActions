using System;
using System.Threading.Tasks;

namespace GuardedActions.ExceptionHandlers.Interfaces
{
    public interface IExceptionHandler
    {
        Task Handle(IExceptionHandlingAction exception);
        Task<bool> CanHandle(Exception exception);
    }

    public interface IExceptionHandler<in TException> : IExceptionHandler where TException : Exception
    {
        Task Handle(IExceptionHandlingAction<TException> exceptionHandlingAction);
    }

    public interface IExceptionHandlingAction
    {
        bool HandlingShouldFinish { get; set; }
    }
}
