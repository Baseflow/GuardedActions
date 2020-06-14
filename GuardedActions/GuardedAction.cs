﻿using System.Threading.Tasks;
using GuardedActions.Interfaces;
using GuardedActions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActions
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

        protected virtual void AssignContextToExceptionHandlers(object context)
        {
            ExceptionGuard.AssignContextToValidExceptionHandlers(context);
        }

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

        protected virtual void AssignContextToExceptionHandlers(object context)
        {
            ExceptionGuard.AssignContextToValidExceptionHandlers(context);
        }

        public virtual void Dispose()
        {
            ExceptionGuard = null;
            //Messenger = null;
        }
    }
}