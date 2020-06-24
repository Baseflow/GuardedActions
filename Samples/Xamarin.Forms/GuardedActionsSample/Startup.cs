using System;
using System.Reflection;
using GuardedActions;
using GuardedActions.Extensions;
using GuardedActionsSample.Factories;
using GuardedActionsSample.Factories.Contracts;
using GuardedActionsSample.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xamarin.Essentials;

namespace GuardedActionsSample
{
    public class Startup
    {
        public static IServiceProvider Services { get; set; }

        public static void Init()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.appsettings.json");

#pragma warning disable IDISP001 // Dispose created.
            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonStream(stream);
                })
                .ConfigureLogging((l) => l.AddConsole(o =>
                {
                    o.DisableColors = true;
                }))
                .ConfigureServices(ConfigureServices)
                .ConfigureGuardedActions(nameof(GuardedActionsSample))
                .Build();
#pragma warning restore IDISP001 // Dispose created.

            //Save our service provider so we can use it later.
            Services = host.Services;

            // TODO: Search if there is a better solution..
            Configuration.SetupServiceProvider(Services);
        }

        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
        {
            ConfigureViewModels(serviceCollection);
            ConfigureFactories(serviceCollection);
        }

        private static void ConfigureViewModels(IServiceCollection serviceCollection)
        {
            foreach (var viewModel in AppDomain.CurrentDomain.GetAssemblies().GetCreatableTypes(typeof(BaseViewModel)))
            {
                serviceCollection.AddTransient(viewModel);
            }
        }

        private static void ConfigureFactories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDownloadFactory, DownloadFactory>();
        }
    }
}
