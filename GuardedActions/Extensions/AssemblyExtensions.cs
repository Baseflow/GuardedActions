using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace GuardedActions.Extensions
{
    // TODO: Seperate NuGet?
    public static class AssemblyExtensions
    {
        private static readonly Dictionary<string, IEnumerable<Type>> _assembliesTypes = new Dictionary<string, IEnumerable<Type>>();

        public static IEnumerable<Type> GetCreatableTypes(this Assembly[] assemblies) => assemblies.AsEnumerable().GetCreatableTypes();

        public static IEnumerable<Type> GetCreatableTypes(this IEnumerable<Assembly> assemblies) => assemblies.SelectMany(GetCreatableTypes);

        public static IEnumerable<Type> GetCreatableTypes(this Assembly assembly)
        {
            if (assembly == null)
                return Array.Empty<Type>();

            if (!_assembliesTypes.ContainsKey(assembly.FullName))
            {
                var types = assembly
                    .ExceptionSafeGetTypes()
                    .Select(t => t.GetTypeInfo())
                    .Where(t => !t.IsAbstract && t.DeclaredConstructors.Any(c => !c.IsStatic && c.IsPublic))
                    .Select(t => t.AsType());

                _assembliesTypes.Add(assembly.FullName, types);
            }

            return _assembliesTypes[assembly.FullName];
        }

        public static IEnumerable<Type> GetCreatableTypes(this Assembly[] assemblies, Type inheritedFrom) => assemblies.AsEnumerable().GetCreatableTypes(inheritedFrom);

        public static IEnumerable<Type> GetCreatableTypes(this IEnumerable<Assembly> assemblies, Type inheritedFrom) => assemblies.SelectMany(a => a.GetCreatableTypes(inheritedFrom));

        public static IEnumerable<Type> GetCreatableTypes(this Assembly assembly, Type inheritedFrom)
        {
            if (assembly == null)
                return Array.Empty<Type>();

            return assembly.GetCreatableTypes().Inherits(inheritedFrom);
        }

        private static IEnumerable<Type> ExceptionSafeGetTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // MvxLog.Instance can be null, when reflecting for Setup.cs
                // Check for null
                //...LogError($"ReflectionTypeLoadException masked during loading of {assembly.FullName} - error {ex.ToLongString()}");

                if (ex.LoaderExceptions != null)
                {
                    foreach (var excp in ex.LoaderExceptions)
                    {
                        //...LogWarn(ex.Message);
                    }
                }

                if (Debugger.IsAttached)
                    Debugger.Break();

                return Array.Empty<Type>();
            }
        }
    }
}
