using System.Threading.Tasks;
using GuardedActions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using GuardedActions.Commands.Actions.Contracts;
using GuardedActions.Commands.Contracts;
using System;

namespace GuardedActions.Commands.Actions
{
    public abstract class GuardedAction : IGuardedAction
    {
        protected GuardedAction()
        {
            // See if DI could be used
            ExceptionGuard = Configuration.Services.GetService<IExceptionGuard>();
            //Messenger = ??
        }

        protected IExceptionGuard ExceptionGuard { get; private set; }
        //TODO find some other solution to inject a messenger or something
        //protected IMvxMessenger Messenger { get; private set; }

        protected abstract Task Execute();

        public async Task ExecuteGuarded()
        {
            await ExceptionGuard.Guard(
                this,
                async () => await Execute().ConfigureAwait(false),
                ExecuteAfter
            ).ConfigureAwait(false);
        }

        protected virtual Task ExecuteAfter() => Task.CompletedTask;

        public virtual void Dispose()
        {
            ExceptionGuard = null;
            //Messenger = null;
        }
    }

    public abstract class GuardedAction<TParameter> : IGuardedAction<TParameter>
    {
        protected GuardedAction()
        {
            ExceptionGuard = Configuration.Services.GetService<IExceptionGuard>();
            //Messenger = ??
        }

        protected IExceptionGuard ExceptionGuard { get; private set; }
        //protected IMvxMessenger Messenger { get; private set; }

        protected abstract Task Execute(TParameter parameter);

        public async Task ExecuteGuarded(TParameter parameter)
        {
            await ExceptionGuard.Guard(
                this,
                async () => await Execute(parameter).ConfigureAwait(false),
                ExecuteAfter
            ).ConfigureAwait(false);
        }

        protected virtual Task ExecuteAfter() => Task.CompletedTask;

        public virtual void Dispose()
        {
            ExceptionGuard = null;
            //Messenger = null;
        }
    }

    public abstract class GuardedDataContextAction<TDataContext> : GuardedAction, IGuardedDataContextAction<TDataContext>
        where TDataContext : class
    {
        public virtual void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public new Task ExecuteGuarded()
        {
            if (DataContext == null)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(ExecuteGuarded)}: {nameof(DataContext)} cannot be null make sure to register the {nameof(DataContext)} by calling the {nameof(RegisterDataContext)} method before you call the {nameof(ExecuteGuarded)} method. Maybe look if you need to override the RegisterDataContext on your command builder class and pass the data source trough to this instance.");

            return base.ExecuteGuarded();
        }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }

    public abstract class GuardedDataContextAction<TDataContext, TParameter> : GuardedAction<TParameter>, IGuardedDataContextAction<TDataContext, TParameter>
        where TDataContext : class
    {
        public virtual void RegisterDataContext(TDataContext dataContext) => DataContext = dataContext;

        public TDataContext DataContext { get; private set; }

        public new Task ExecuteGuarded(TParameter parameter)
        {
            if (DataContext == null)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(ExecuteGuarded)}({nameof(TParameter)}): {nameof(DataContext)} cannot be null make sure to register the {nameof(DataContext)} by calling the {nameof(RegisterDataContext)} method before you call the {nameof(ExecuteGuarded)} method. Maybe look if you need to override the RegisterDataContext on your command builder class and pass the data source trough to this instance.");

            return base.ExecuteGuarded(parameter);
        }

        public override void Dispose()
        {
            base.Dispose();
            DataContext = null;
        }
    }
}
