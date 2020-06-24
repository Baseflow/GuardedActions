using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GuardedActions.IoC;

namespace GuardedActions.Extensions
{
    // TODO: Seperate NuGet?
    public static class TypeExtensions
    {
        public static IEnumerable<Type> Inherits(this IEnumerable<Type> types, Type baseType)
        {
            return types.Where(x => baseType.IsAssignableFrom(x));
        }

        public static IEnumerable<Type> Inherits<TBase>(this IEnumerable<Type> types) => types.Inherits(typeof(TBase));

        public static IEnumerable<Type> EndingWith(this IEnumerable<Type> types, string endingWith)
        {
            return types.Where(x => x.Name.EndsWith(endingWith, StringComparison.Ordinal));
        }

        public static IEnumerable<Type> GetImplementedInterfaces(this Type type, bool excludeBaseTypeInterfaces = false)
        {
            if (type == null)
                return Array.Empty<Type>();

            var interfaces = type.GetTypeInfo().ImplementedInterfaces.Where(i => i != typeof(IDisposable)).ToList();

            if (!excludeBaseTypeInterfaces)
                return interfaces;

            var baseTypeInterfaces = type.BaseType.GetImplementedInterfaces();
            return interfaces.Except(baseTypeInterfaces);
        }
    }
}
