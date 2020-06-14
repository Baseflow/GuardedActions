using System;
using AsyncAwaitBestPractices.MVVM;

namespace GuardedActions.Commands.Interfaces
{
    public interface IAsyncGuardedCommandBuilder : ICommandBuilder
    {
        IAsyncCommand BuildCommand();

        IAsyncGuardedCommandBuilder EnqueueAfterCommandExecuted(Action taskToInvoke);
        IAsyncGuardedCommandBuilder EnqueueBeforeCommandExecuted(Action taskToInvoke);
    }

    public interface IAsyncGuardedCommandBuilder<TCommandParameter> : ICommandBuilder
    {
        IAsyncCommand<TCommandParameter> BuildCommand();

        IAsyncGuardedCommandBuilder<TCommandParameter> EnqueueAfterCommandExecuted(Action taskToInvoke);
        IAsyncGuardedCommandBuilder<TCommandParameter> EnqueueBeforeCommandExecuted(Action taskToInvoke);
    }
}
