using System;
using GuardedActions.IoC;
using MvvmCross.IoC;

namespace GuardedActions.MvvmCross
{
    public class IoCSetup : IoCRegistration
    {
        private IMvxIoCProvider? _provider;

        public void Configure(IMvxIoCProvider provider, params string[] assemblyNames)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));

            Register(assemblyNames);
        }

        public override void AddSingletonInternal<TServiceType>(Func<TServiceType> constructor) where TServiceType : class => _provider.RegisterSingleton(() => constructor.Invoke());

        public override void AddSingletonInternal(Type serviceType)
        {
            var instance = _provider.IoCConstruct(serviceType);
            _provider.RegisterSingleton(serviceType, instance);
        }

        public override void AddSingletonInternal(Type contractType, Type serviceType)
        {
            var instance = _provider.IoCConstruct(serviceType);
            _provider.RegisterSingleton(contractType, instance);
        }

        public override void AddTransientInternal(Type serviceType) => _provider.RegisterType(serviceType);

        public override void AddTransientInternal(Type contractType, Type serviceType) => _provider.RegisterType(contractType, serviceType);

        public override TServiceType GetServiceInternal<TServiceType>() where TServiceType : class => _provider.Resolve<TServiceType>();

        public override TServiceType GetServiceInternal<TServiceType>(Type serviceType) where TServiceType : class => (TServiceType) _provider.Resolve(serviceType);

        public override bool CanRegister => _provider != null;

        public override bool CanResolve => _provider != null;

        public override string CannotRegisterErrorMessage => $"Make sure you've called the {nameof(Configure)} on the {nameof(IoCSetup)} before the AppStart.";

        public override string CannotResolveErrorMessage => CannotRegisterErrorMessage;
    }
}
