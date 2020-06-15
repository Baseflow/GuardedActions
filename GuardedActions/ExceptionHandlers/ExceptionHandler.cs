using System;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers
{
    public abstract class ExceptionHandler<TException> : IExceptionHandler<TException> where TException : Exception
    {
        public abstract Task Handle(IExceptionHandlingAction<TException> exceptionHandlingAction);
        public virtual Task<bool> CanHandle(TException exception) => Task.FromResult(exception != null);
        Task IExceptionHandler.Handle(IExceptionHandlingAction exceptionHandlingAction) => Handle(exceptionHandlingAction as IExceptionHandlingAction<TException>);
        Task<bool> IExceptionHandler.CanHandle(Exception exception) => CanHandle(exception as TException);
    }

    public abstract class ContextExceptionHandler<TException, TContext> : ExceptionHandler<TException>, IContextExceptionHandler<TException, TContext> where TException : Exception where TContext : class
    {
        public WeakReference<TContext> Context { get; } = new WeakReference<TContext>(null);
    }

    public interface IContextExceptionHandler<TException, TContext> : IExceptionHandler<TException>
        where TException : Exception where TContext : class
    {
        WeakReference<TContext> Context { get; }
    }
}
