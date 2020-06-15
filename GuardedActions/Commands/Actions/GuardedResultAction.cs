using System.Threading.Tasks;
using GuardedActions.Commands.Actions.Contracts;
using GuardedActions.Extensions;
using GuardedActions.ExceptionGuards.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActions.Commands.Actions
{
    public abstract class GuardedResultAction<TResult> : IGuardedResultAction<TResult>
    {
        protected GuardedResultAction()
        {
            ExceptionGuard = Configuration.Services.GetService<IExceptionGuard>();
        }

        protected IExceptionGuard ExceptionGuard { get; private set; }

        protected abstract Task<TResult> Execute();

        public async Task<TResult> ExecuteGuarded()
        {
            return await ExceptionGuard.Guard(
                this,
                async () => await Execute().ConfigureAwait(false),
                ExecuteAfter
            ).ConfigureAwait(false);
        }

        protected virtual Task ExecuteAfter() => Task.CompletedTask;

        public virtual void Dispose()
        {
            ExceptionGuard = null;
        }
    }

    public abstract class GuardedResultAction<TParameter, TResult> : IGuardedResultAction<TParameter, TResult>
    {
        protected GuardedResultAction()
        {
            ExceptionGuard = Configuration.Services.GetService<IExceptionGuard>();
        }

        protected IExceptionGuard ExceptionGuard { get; private set; }

        protected abstract Task<TResult> Execute(TParameter parameter);

        public async Task<TResult> ExecuteGuarded(TParameter parameter)
        {
            return await ExceptionGuard.Guard(
                this,
                async () => await Execute(parameter).ConfigureAwait(false),
                ExecuteAfter
            ).ConfigureAwait(false);
        }

        protected virtual Task ExecuteAfter() => Task.CompletedTask;

        public virtual void Dispose()
        {
            ExceptionGuard = null;
        }
    }
}
