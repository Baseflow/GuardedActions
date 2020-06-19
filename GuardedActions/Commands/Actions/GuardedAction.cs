using System.Threading.Tasks;
using GuardedActions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using GuardedActions.Commands.Actions.Contracts;
using GuardedActions.Commands.Contracts;

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
}
