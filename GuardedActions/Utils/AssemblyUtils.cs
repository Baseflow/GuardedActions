using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GuardedActions.Utils
{
    public static class AssemblyUtils
    {
        public static IEnumerable<Assembly> GetRegistrationAssemblies(params string[] assemblyNames)
        {
            var assemblies = GetAssembliesWhereNameStartingWith(assemblyNames).ToList();

            assemblies.Add(Assembly.GetExecutingAssembly());

            return assemblies;
        }

        public static IEnumerable<Assembly> GetAssembliesWhereNameStartingWith(string assemblyName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => assembly.GetName().Name.StartsWith(assemblyName, StringComparison.InvariantCulture));
        }

        public static IEnumerable<Assembly> GetAssembliesWhereNameStartingWith(params string[] assemblyNames)
        {
            return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(assembly =>
                        assemblyNames.Any(assemblyName =>
                            assembly.GetName().Name.StartsWith(assemblyName, StringComparison.InvariantCulture)));
        }

        public static Assembly GetAssemblyByName(string assemblyName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName);
        }
    }
}
