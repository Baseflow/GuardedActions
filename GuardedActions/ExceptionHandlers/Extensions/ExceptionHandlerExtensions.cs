using System;
using System.Linq;
using GuardedActions.ExceptionHandlers.Contracts;
using GuardedActions.Extensions;

namespace GuardedActions.ExceptionHandlers.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static bool Implements(this IExceptionHandler exceptionHandler, Exception exception)
        {
            if (exceptionHandler == null)
                return false;

            var type = exceptionHandler.GetExceptionType();
            if (type == null)
                return false;
            return type.IsInstanceOfType(exception);
        }

        public static Type? GetExceptionType(this IExceptionHandler exceptionHandler)
        {
            if (exceptionHandler == null)
                return null;

            return exceptionHandler.GetType().GetImplementedInterfaces().FirstOrDefault(i =>
                i.Name.StartsWith(nameof(IExceptionHandler), StringComparison.Ordinal) &&
                i.IsGenericType
            )?.GenericTypeArguments[0];
        }
    }
}
