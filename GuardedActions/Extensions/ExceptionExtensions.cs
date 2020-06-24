using System;

namespace GuardedActions.Extensions
{
    // TODO: Seperate NuGet?
    public static class ExceptionExtensions
    {
        public static string ToLongString(this Exception exception)
        {
            if (exception == null)
                return "null";

            if (exception.InnerException != null)
            {
                var innerExceptionText = exception.InnerException.ToLongString();
                return $"{exception.GetType().Name}: {exception.Message ?? "-"}\n\t{exception.StackTrace}\nInnerException was {innerExceptionText}";
            }
            return $"{exception.GetType().Name}: {exception.Message ?? "-"}\n\t{exception.StackTrace}";
        }
    }
}
