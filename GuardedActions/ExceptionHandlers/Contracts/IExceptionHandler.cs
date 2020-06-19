using System;
using System.Threading.Tasks;

namespace GuardedActions.ExceptionHandlers.Contracts
{
    public interface IExceptionHandler
    {
        Task Handle(IExceptionHandlingAction exceptionHandlingAction);
        Task<bool> CanHandle(IExceptionHandlingAction exceptionHandlingAction);
    }

    public interface IExceptionHandler<in TException> : IExceptionHandler
        where TException : Exception
    {
        Task<bool> CanHandle(IExceptionHandlingAction<TException> exceptionHandlingAction);
        Task Handle(IExceptionHandlingAction<TException> exceptionHandlingAction);
    }

    public interface IExceptionHandler<in TException, TDataContext> : IExceptionHandler
        where TException : Exception
        where TDataContext : class
    {
        Task<bool> CanHandle(IExceptionHandlingAction<TException, TDataContext> exceptionHandlingAction);
        Task Handle(IExceptionHandlingAction<TException, TDataContext> exceptionHandlingAction);
    }
}
