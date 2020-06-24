using System;
using System.Threading.Tasks;
using GuardedActions.Commands.Actions.Contracts;
using GuardedActions.Commands.Contracts;
using GuardedActions.Extensions;
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

    public abstract class GuardedDataContextResultAction<TDataContext, TResult> : GuardedResultAction<TResult>,
        IGuardedDataContextResultAction<TDataContext, TResult>
        where TDataContext : class
    {
        protected GuardedDataContextResultAction()
        {
        }

        public new Task ExecuteGuarded()
        {
            if (DataContext == null)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(ExecuteGuarded)}(): {nameof(DataContext)} cannot be null make sure to register the {nameof(DataContext)} by calling the {nameof(RegisterDataContext)} method before you call the {nameof(ExecuteGuarded)} method. Maybe look if you need to override the RegisterDataContext on your command builder class and pass the data source trough to this instance.");

            return base.ExecuteGuarded();
        }

        public void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public virtual void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }

    public abstract class GuardedDataContextResultAction<TDataContext, TParameter, TResult> : GuardedResultAction<TParameter, TResult>,
        IGuardedDataContextResultAction<TDataContext, TParameter, TResult>
        where TDataContext : class
    {
        protected GuardedDataContextResultAction()
        {
        }

        public new Task ExecuteGuarded(TParameter parameter)
        {
            if (DataContext == null)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(ExecuteGuarded)}({nameof(TParameter)}): {nameof(DataContext)} cannot be null make sure to register the {nameof(DataContext)} by calling the {nameof(RegisterDataContext)} method before you call the {nameof(ExecuteGuarded)} method. Maybe look if you need to override the RegisterDataContext on your command builder class and pass the data source trough to this instance.");

            return base.ExecuteGuarded(parameter);
        }

        public void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
