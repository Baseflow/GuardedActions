using System;
using GuardedActions.Extensions;
using GuardedActions.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuardedActions
{
    public static class Configuration
    {
        private static bool _servicesInitialized;

        public static IServiceProvider Services { get; private set; }

        public static void SetupServiceProvider(IServiceProvider serviceProvider)
        {
            if (!_servicesInitialized)
                throw new InvalidOperationException($"The {nameof(HostBuilderExtensions.ConfigureGuardedActions)} method on the {nameof(IHostBuilder)} has not been invoked.");

            Services = serviceProvider;
        }

        internal static void ConfigureServices(IServiceCollection serviceCollection, params string[] assemblyNames)
        {
            new IoCRegistration(serviceCollection, assemblyNames).Register();

            _servicesInitialized = true;
        }
    }
}
