using System;
using GuardedActions.ExceptionHandlers.Contracts;

namespace GuardedActions.ExceptionHandlers
{
    public class ExceptionHandlingActionFactory : IExceptionHandlingActionFactory
    {
        public IExceptionHandlingAction Create(Exception exception, object context = null)
        {
            if (exception == null)
            {
                // write log.
                return null;
            }

            if (context == null)
            {
                var genericType = typeof(ExceptionHandlingAction<>).MakeGenericType(exception.GetType());

                return Activator.CreateInstance(genericType, exception) as IExceptionHandlingAction;
            }

            var genericContextType = typeof(ExceptionHandlingAction<,>).MakeGenericType(exception.GetType(), context.GetType());

            return Activator.CreateInstance(genericContextType, exception, context) as IExceptionHandlingAction;
        }
    }
}
