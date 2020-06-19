using System;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers
{
    public abstract class ExceptionHandler<TException> : IExceptionHandler<TException>
        where TException : Exception
    {
        public virtual Task<bool> CanHandle(IExceptionHandlingAction<TException> exceptionHandlingAction) => Task.FromResult(exceptionHandlingAction?.Exception != null);

        public Task<bool> CanHandle(IExceptionHandlingAction exceptionHandlingAction) => CanHandle(exceptionHandlingAction as IExceptionHandlingAction<TException>);

        public abstract Task Handle(IExceptionHandlingAction<TException> exceptionHandlingAction);

        public Task Handle(IExceptionHandlingAction exceptionHandlingAction) => Handle(exceptionHandlingAction as IExceptionHandlingAction<TException>);
    }

    public abstract class ContextExceptionHandler<TException, TDataContext> : IExceptionHandler<TException, TDataContext>
        where TException : Exception
        where TDataContext : class
    {
        public virtual Task<bool> CanHandle(IExceptionHandlingAction<TException, TDataContext> exceptionHandlingAction) =>
            Task.FromResult(exceptionHandlingAction?.Exception != null && exceptionHandlingAction?.DataContext != null);

        public Task<bool> CanHandle(IExceptionHandlingAction exceptionHandlingAction) => CanHandle(exceptionHandlingAction as IExceptionHandlingAction<TException, TDataContext>);

        public abstract Task Handle(IExceptionHandlingAction<TException, TDataContext> exceptionHandlingAction);

        public Task Handle(IExceptionHandlingAction exceptionHandlingAction) => Handle(exceptionHandlingAction as IExceptionHandlingAction<TException, TDataContext>);
    }
}
