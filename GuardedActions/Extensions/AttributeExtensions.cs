using System;
using System.Reflection;

namespace GuardedActions.Extensions
{
    // TODO: Seperate NuGet?
    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this object obj) where T: Attribute
        {
            if (obj == null)
                return null;

            return Attribute.GetCustomAttribute(obj.GetType(), typeof(T)) is T attr ? attr : default;
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return Attribute.GetCustomAttribute(type, typeof(T)) is T attr ? attr : default;
        }

        public static T GetAttribute<T>(this Assembly assembly) where T : Attribute
        {
            return assembly.GetCustomAttribute<T>();
        }
    }
}