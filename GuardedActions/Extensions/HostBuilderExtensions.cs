using Microsoft.Extensions.Hosting;

namespace GuardedActions.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureGuardedActions(this IHostBuilder builder, params string[] assemblyNames) =>
            builder?.ConfigureServices((_, sc) => Configuration.ConfigureServices(sc, assemblyNames));
    }
}
