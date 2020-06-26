using System;
using GuardedActions.NetCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuardedActions.NetCore
{
    public class IoCRegistration : IoC.IoCRegistration
    {
        private IServiceCollection? _serviceCollection;
        private IServiceProvider? _serviceProvider;

        public IoCRegistration SetServiceCollection(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
            return this;
        }

        public IoCRegistration SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            return this;
        }

        public override void AddSingletonInternal<TServiceType>(Func<TServiceType> constructor) where TServiceType : class => _serviceCollection.AddSingleton((_) => constructor.Invoke());

        public override void AddSingletonInternal(Type serviceType) => _serviceCollection.AddSingleton(serviceType);

        public override void AddSingletonInternal(Type serviceType, Type implementationType) => _serviceCollection.AddSingleton(serviceType, implementationType);

        public override void AddTransientInternal(Type serviceType) => _serviceCollection.AddTransient(serviceType);

        public override void AddTransientInternal(Type serviceType, Type implementationType) => _serviceCollection.AddTransient(serviceType, implementationType);

        public override TServiceType GetServiceInternal<TServiceType>() where TServiceType : class => _serviceProvider.GetService<TServiceType>();

        public override TServiceType GetServiceInternal<TServiceType>(Type serviceType) where TServiceType : class => _serviceProvider.GetService<TServiceType>(serviceType);

        public override bool CanRegister => _serviceCollection != null;

        public override bool CanResolve => _serviceProvider != null;

        public override string CannotRegisterErrorMessage => $"Make sure you've called the {nameof(HostBuilderExtensions.ConfigureGuardedActions)} on the {nameof(IHostBuilder)} before building it.";

        public override string CannotResolveErrorMessage => $"Make sure you've called the {nameof(HostBuilderExtensions.ConnectGuardedActions)} on the {nameof(IHost)} after building the {nameof(IHostBuilder)}.";
    }
}
