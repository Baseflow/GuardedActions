﻿using System.Threading.Tasks;

namespace GuardedActions.Commands.Actions.Contracts
{
    public interface IGuardedResultAction<TParameter, TResult> : IAction
    {
        Task<TResult> ExecuteGuarded(TParameter parameter);
    }

    public interface IGuardedResultAction<TResult> : IAction
    {
        Task<TResult> ExecuteGuarded();
    }
}