﻿using System;
using System.Threading.Tasks;

namespace GuardedActions.ExceptionHandlers.Contracts
{
    public interface IExceptionHandler
    {
        Task Handle(IExceptionHandlingAction exception);
        Task<bool> CanHandle(Exception exception);
    }

    public interface IExceptionHandler<in TException> : IExceptionHandler where TException : Exception
    {
        Task Handle(IExceptionHandlingAction<TException> exceptionHandlingAction);
    }

    public interface IContextExceptionHandler<TException, TContext> : IExceptionHandler<TException> where TException : Exception where TContext : class
    {
        WeakReference<TContext> Context { get; }
    }
}
