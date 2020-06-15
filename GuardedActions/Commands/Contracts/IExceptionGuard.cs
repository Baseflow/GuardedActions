using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.Commands.Contracts
{
    public interface IExceptionGuard
    {
        Task Guard(object sender, Func<Task> job, Func<Task> onFinally = null);
        Task<TResult> Guard<TResult>(object sender, Func<Task<TResult>> job, Func<Task> onFinally = null);
        List<IExceptionHandler> ExceptionHandlers { get; }
        void SetContextFor<THandler>(object context) where THandler : IExceptionHandler;
        void AssignContextToValidExceptionHandlers(object context);
    }
}
