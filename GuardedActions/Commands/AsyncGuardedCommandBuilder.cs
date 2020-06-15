using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using GuardedActions.Commands.Contracts;
using GuardedActions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActions.Commands
{
    public abstract class AsyncGuardedCommandBuilder : IAsyncGuardedCommandBuilder
    {
        protected Queue<Action> InvokeAfterCommandExecutedTaskQueue { get; } = new Queue<Action>();
        protected Queue<Action> InvokeBeforeCommandExecutedTaskQueue { get; } = new Queue<Action>();
        protected bool CommandBeingExecuted { get; set; }
        protected Func<bool> CanExecuteFunction { get; set; }

        protected AsyncGuardedCommandBuilder()
        {
            ExceptionGuard = Configuration.Services.GetService<IExceptionGuard>();
            //Messenger = ??
        }

        protected virtual bool ShouldNotifyAboutLongOperation { get; } = true;

        // TODO: Find framework independent solution
        //public IMvxMessenger Messenger { get; protected set; }
        public IExceptionGuard ExceptionGuard { get; protected set; }

        public IAsyncGuardedCommandBuilder EnqueueAfterCommandExecuted(Action taskToInvoke)
        {
            InvokeAfterCommandExecutedTaskQueue.Enqueue(taskToInvoke);
            return this;
        }

        public IAsyncGuardedCommandBuilder EnqueueBeforeCommandExecuted(Action taskToInvoke)
        {
            InvokeBeforeCommandExecutedTaskQueue.Enqueue(taskToInvoke);
            return this;
        }

        public AsyncGuardedCommandBuilder ConfigureCanExecute(Func<bool> canExecute)
        {
            CanExecuteFunction = canExecute;
            return this;
        }

        public IAsyncCommand BuildCommand()
        {
            IAsyncCommand command = null;

            // ReSharper disable once AccessToModifiedClosure
            command = new AsyncCommand(() => BuildCommandBody(command), _ =>
            {
                if (CanExecute())
                    return !CommandBeingExecuted;

                return false;
            });

            return command;
        }

        protected virtual async Task BuildCommandBody(IAsyncCommand command)
        {
            await ExceptionGuard.Guard(this,
                async () =>
                {
                    if (ShouldNotifyAboutLongOperation)
                    {
                        NotifyAboutLongOperationStarted();
                    }

                    CommandBeingExecuted = true;

                    while (InvokeBeforeCommandExecutedTaskQueue.Count > 0)
                    {
                        await Task.Run(InvokeBeforeCommandExecutedTaskQueue.Dequeue()).ConfigureAwait(false);
                    }

                    command.RaiseCanExecuteChanged();

                    AssignContextToExceptionHandlers(this);

                    await ExecuteCommandAction().ConfigureAwait(false);
                },
                async () =>
                {
                    if (ShouldNotifyAboutLongOperation)
                    {
                        NotifyAboutLongOperationFinished();
                    }

                    CommandBeingExecuted = false;
                    command.RaiseCanExecuteChanged();
                    Finally();

                    while (InvokeAfterCommandExecutedTaskQueue.Count > 0)
                    {
                        await Task.Run(InvokeAfterCommandExecutedTaskQueue.Dequeue()).ConfigureAwait(false);
                    }
                }
            ).ConfigureAwait(false);
        }

        protected virtual void NotifyAboutLongOperationStarted()
        {
            // Find framework independent solution
            //Messenger.Publish(LongRunningAsyncOperationMessage.Started(this));
        }

        protected virtual void NotifyAboutLongOperationFinished()
        {
            // TODO: Find framework independent solution
            //Messenger.Publish(LongRunningAsyncOperationMessage.Finished(this));
        }

        protected virtual void Finally()
        {
        }

        protected abstract Task ExecuteCommandAction();

        protected virtual bool CanExecute()
        {
            return CanExecuteFunction?.Invoke() != false;
        }

        protected virtual void AssignContextToExceptionHandlers(object context)
        {
            ExceptionGuard.AssignContextToValidExceptionHandlers(context);
        }

        public virtual void Dispose()
        {
            ExceptionGuard = null;

            // TODO: Find framework independent solution
            //Messenger = null;
        }
    }

    public abstract class AsyncGuardedCommandBuilder<TCommandParameter> : IAsyncGuardedCommandBuilder<TCommandParameter>
    {
        protected Queue<Action> InvokeAfterCommandExecutedTaskQueue { get; } = new Queue<Action>();
        protected Queue<Action> InvokeBeforeCommandExecutedTaskQueue { get; } = new Queue<Action>();
        protected bool CommandBeingExecuted { get; set; }
        protected Func<TCommandParameter, bool> CanExecuteFunction { get; set; }

        protected AsyncGuardedCommandBuilder()
        {
            ExceptionGuard = Configuration.Services.GetService<IExceptionGuard>();
            //Messenger = ??
        }

        protected virtual bool ShouldNotifyAboutLongOperation { get; } = true;
        //public IMvxMessenger Messenger { get; protected set; }
        public IExceptionGuard ExceptionGuard { get; protected set; }

        public IAsyncGuardedCommandBuilder<TCommandParameter> EnqueueAfterCommandExecuted(Action taskToInvoke)
        {
            InvokeAfterCommandExecutedTaskQueue.Enqueue(taskToInvoke);
            return this;
        }

        public IAsyncGuardedCommandBuilder<TCommandParameter> EnqueueBeforeCommandExecuted(Action taskToInvoke)
        {
            InvokeBeforeCommandExecutedTaskQueue.Enqueue(taskToInvoke);
            return this;
        }

        public AsyncGuardedCommandBuilder<TCommandParameter> ConfigureCanExecute(Func<TCommandParameter, bool> canExecute)
        {
            CanExecuteFunction = canExecute;
            return this;
        }

        public IAsyncCommand<TCommandParameter> BuildCommand()
        {
            IAsyncCommand<TCommandParameter> command = null;

            command = new AsyncCommand<TCommandParameter>(async model => await BuildCommandBody(command, model).ConfigureAwait(false), p =>
            {
                if (p is TCommandParameter commandParameter)
                {
                    var canExecute = CanExecute(commandParameter);
                    if (canExecute)
                    {
                        return !CommandBeingExecuted;
                    }
                }
                else
                {
                    // This should never happen
                }
                return false;
            });

            return command;
        }

        protected virtual async Task BuildCommandBody(IAsyncCommand<TCommandParameter> command, TCommandParameter model)
        {
            await ExceptionGuard.Guard(this,
                async () =>
                {
                    if (ShouldNotifyAboutLongOperation)
                    {
                        NotifyAboutLongOperationStarted();
                    }

                    CommandBeingExecuted = true;

                    while (InvokeBeforeCommandExecutedTaskQueue.Any())
                    {
                        await Task.Run(InvokeBeforeCommandExecutedTaskQueue.Dequeue()).ConfigureAwait(false);
                    }

                    command.RaiseCanExecuteChanged();

                    AssignContextToExceptionHandlers(this);

                    await ExecuteCommandAction(model).ConfigureAwait(false);
                },
                async () =>
                {
                    if (ShouldNotifyAboutLongOperation)
                    {
                        NotifyAboutLongOperationFinished();
                    }

                    CommandBeingExecuted = false;
                    command.RaiseCanExecuteChanged();
                    Finally();

                    while (InvokeAfterCommandExecutedTaskQueue.Any())
                    {
                        await Task.Run(InvokeAfterCommandExecutedTaskQueue.Dequeue()).ConfigureAwait(false);
                    }
                }
            ).ConfigureAwait(false);
        }

        protected virtual void NotifyAboutLongOperationStarted()
        {
            // TODO: Find framework independent solution
            //Messenger.Publish(LongRunningAsyncOperationMessage.Started(this));
        }

        protected virtual void NotifyAboutLongOperationFinished()
        {
            // TODO: Find framework independent solution
            //Messenger.Publish(LongRunningAsyncOperationMessage.Finished(this));
        }

        protected virtual void Finally()
        {
        }

        protected virtual void AssignContextToExceptionHandlers(object context)
        {
            ExceptionGuard.AssignContextToValidExceptionHandlers(context);
        }

        protected virtual bool CanExecute(TCommandParameter item)
        {
            return CanExecuteFunction == null || CanExecuteFunction.Invoke(item);
        }

        protected abstract Task ExecuteCommandAction(TCommandParameter item);

        public virtual void Dispose()
        {
            ExceptionGuard = null;
            // TODO: Find framework independent solution
            //Messenger = null;
        }
    }
}
