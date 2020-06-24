using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardedActions.ExceptionHandlers.Attributes;
using GuardedActions.ExceptionHandlers.Defaults;
using GuardedActions.ExceptionHandlers.Contracts;
using GuardedActions.ExceptionHandlers.Extensions;
using GuardedActions.Extensions;
using GuardedActions.Commands.Contracts;

namespace GuardedActions.Commands
{
    public class ExceptionGuard : IExceptionGuard
    {
        private readonly IExceptionHandlingActionFactory _exceptionHandlingActionFactory;

        public ExceptionGuard(IEnumerable<IExceptionHandler> handlers, IExceptionHandlingActionFactory exceptionHandlingActionFactory)
        {
            _exceptionHandlingActionFactory = exceptionHandlingActionFactory;

            ExceptionHandlers.AddRange(handlers);
        }

        public List<IExceptionHandler> ExceptionHandlers { get; } = new List<IExceptionHandler>();

        private IExceptionHandler this[Type type] => ExceptionHandlers.SingleOrDefault(h =>
            h.GetType().GetInterfaces().FirstOrDefault(i => i == type) != null ||
            h.GetType() == type
        );

        public async Task Guard(object sender, Func<Task> job, Func<Task> onFinally = null)
        {
            try
            {
                if (job == null)
                    throw new InvalidOperationException($"{GetType().FullName}.{nameof(Guard)}(): The {nameof(job)} provided cannot be null.");

                await job().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await HandleException(sender, exception).ConfigureAwait(false);
            }
            finally
            {
                if (onFinally != null)
                {
                    await onFinally().ConfigureAwait(false);
                }
            }
        }

        public async Task<TResult> Guard<TResult>(object sender, Func<Task<TResult>> job, Func<Task> onFinally = null)
        {
            TResult result = default;
            try
            {
                if (job == null)
                    throw new InvalidOperationException($"{GetType().FullName}.{nameof(Guard)}<{nameof(TResult)}>(): The {nameof(job)} provided cannot be null.");

                result = await job().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                await HandleException(sender, exception).ConfigureAwait(false);
            }
            finally
            {
                if (onFinally != null)
                {
                    await onFinally().ConfigureAwait(false);
                }
            }

            return result;
        }

        private async Task HandleException(object sender, Exception exception)
        {
            if (ExceptionHandlers?.Any() != true)
                return;

            GetHandlers(sender, exception, out var fallbackHandler, out var defaultHandlers, out var customHandlers, out var skipDefaultHandlers);

            var context = GetContextFromSender(sender);

            var exceptionHandlingAction = _exceptionHandlingActionFactory.Create(exception, context);

            foreach (var exceptionHandler in DetermineHandlersToUse(fallbackHandler, defaultHandlers, customHandlers, skipDefaultHandlers))
            {
                if (!await exceptionHandler.CanHandle(exceptionHandlingAction).ConfigureAwait(false))
                    continue;

                await exceptionHandler.Handle(exceptionHandlingAction).ConfigureAwait(false);

                if (exceptionHandlingAction.HandlingShouldFinish)
                    break;
            }
        }

        private void GetHandlers(object sender, Exception exception, out IExceptionHandler fallbackHandler, out List<IExceptionHandler> defaultHandlers, out List<IExceptionHandler> customHandlers, out bool skipDefaultHandlers)
        {
            fallbackHandler = null;
            defaultHandlers = new List<IExceptionHandler>();
            customHandlers = new List<IExceptionHandler>();
            skipDefaultHandlers = false;

            var customHandlersDictionary = new SortedDictionary<int, IList<IExceptionHandler>>();

            foreach (var handler in ExceptionHandlers.Where(h => h.Implements(exception)))
            {
                if (handler.GetType().FullName == typeof(GeneralExceptionHandler).FullName)
                {
                    fallbackHandler = handler;
                    continue;
                }

                var defaultHandlerAttribute = handler.GetAttribute<DefaultExceptionHandlerAttribute>();
                if (defaultHandlerAttribute != null)
                {
                    defaultHandlers.Add(handler);
                    continue;
                }

                var handlingOnAttribute = handler.GetAttribute<ExceptionHandlerForAttribute>();
                if (handlingOnAttribute != null)
                {
                    if (!handlingOnAttribute.TypesToHandleOn.Any(t => t.IsInstanceOfType(sender)))
                        continue;

                    if (handlingOnAttribute.SkipDefaultHandlers)
                        skipDefaultHandlers = true;

                    customHandlersDictionary.AddOrCreate(handlingOnAttribute.PriorityLevel, handler);
                }
            }

            foreach (var customHandlersDictionaryEntry in customHandlersDictionary.Reverse())
            {
                customHandlers.AddRange(customHandlersDictionaryEntry.Value);
            }
        }

        private static List<IExceptionHandler> DetermineHandlersToUse(IExceptionHandler fallbackHandler, List<IExceptionHandler> defaultHandlers, List<IExceptionHandler> customHandlers, bool skipDefaultHandlers)
        {
            var handlersToUse = new List<IExceptionHandler>();

            if (customHandlers.Count == 0 && defaultHandlers.Count == 0)
            {
                handlersToUse.Add(fallbackHandler);
            }
            else
            {
                if (!skipDefaultHandlers)
                {
                    if (defaultHandlers.Count > 0)
                        handlersToUse.AddRange(defaultHandlers);
                    else
                        handlersToUse.Add(fallbackHandler);
                }

                handlersToUse.AddRange(customHandlers);
            }

            return handlersToUse;
        }

        private static object? GetContextFromSender(object sender)
        {
            return sender?.GetType()?.GetProperties()?.FirstOrDefault(f => f.Name == "DataContext")?.GetValue(sender);
        }
    }
}
