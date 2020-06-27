using System;
using Microsoft.Extensions.Hosting;

namespace GuardedActions.NetCore.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureGuardedActions(this IHostBuilder builder, IoCSetup iocRegistration, params string[] assemblyNames)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (iocRegistration == null)
                throw new ArgumentNullException(nameof(iocRegistration));

            return builder.ConfigureServices((_, sc) =>
            {
                iocRegistration.SetServiceCollection(sc);
                iocRegistration.Register(assemblyNames);
            });
        }

        public static IHost ConnectGuardedActions(this IHost host, IoCSetup ioCRegistration)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));
            if (ioCRegistration == null)
                throw new ArgumentNullException(nameof(ioCRegistration));

            ioCRegistration.SetServiceProvider(host.Services);

            return host;
        }
    }
}
