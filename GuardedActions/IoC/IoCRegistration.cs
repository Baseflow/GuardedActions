using System;
using System.Collections.Generic;
using GuardedActions.Commands.Actions.Contracts;
using GuardedActions.Commands.Contracts;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Contracts;
using GuardedActions.Extensions;
using GuardedActions.Utils;
using GuardedActions.Commands;
using System.Linq;

namespace GuardedActions.IoC
{
    public abstract class IoCRegistration
    {
        private IEnumerable<Type> _createableTypes = Array.Empty<Type>();

        public static IoCRegistration Instance { get; private set; }

        protected IoCRegistration()
        {
            Instance = this;
        }

        public abstract bool CanRegister { get; }

        public abstract bool CanResolve { get; }

        public void Register(params string[] assemblyNames)
        {
            if (!CanRegister)
                throw new InvalidOperationException($"{GetType().FullName}: {CannotRegisterErrorMessage}.");

            DetermineCreatableTypes(assemblyNames);

            AddSweep<IAction>(IoCRegistrationType.Transient);

            AddSweep<ICommandBuilder>(IoCRegistrationType.Transient);

            var exceptionHandlerTypes = AddSweep<IExceptionHandler>(IoCRegistrationType.Singleton);

            AddSingleton<IExceptionHandlingActionFactory, ExceptionHandlingActionFactory>();

            AddSingleton<IExceptionGuard>(() =>
            {
                var factory = GetService<IExceptionHandlingActionFactory>();
                var handlers = exceptionHandlerTypes.Select(handlerType => GetService<IExceptionHandler>(handlerType)).ToList();
                return new ExceptionGuard(handlers, factory);
            });
        }

        private void DetermineCreatableTypes(string[] assemblyNames)
        {
            var assemblies = assemblyNames != null ? AssemblyUtils.GetRegistrationAssemblies(assemblyNames) : AppDomain.CurrentDomain.GetAssemblies();

            _createableTypes = assemblies.GetCreatableTypes() ?? Array.Empty<Type>();
        }

        protected IEnumerable<Type> AddSweep(string kind, IoCRegistrationType registrationType)
        {
            var types = _createableTypes.EndingWith(kind);

            AddTypes(types, registrationType);

            return types;
        }

        protected IEnumerable<Type> AddSweep(Type inheritedFrom, IoCRegistrationType registrationType)
        {
            var types = _createableTypes.Inherits(inheritedFrom);

            AddTypes(types, registrationType);

            return types;
        }

        protected IEnumerable<Type> AddSweep<T>(IoCRegistrationType registrationType) => AddSweep(typeof(T), registrationType);

        private void AddTypes(IEnumerable<Type> types, IoCRegistrationType registrationType)
        {
            foreach (var type in types)
            {
                if (registrationType == IoCRegistrationType.Manual)
                    continue;

                var implementing = type.GetImplementedInterfaces(true);

                if (registrationType == IoCRegistrationType.Transient)
                {
                    foreach (var implementedType in implementing)
                    {
                        AddTransient(implementedType, type);
                    }
                    AddTransient(type);
                    continue;
                }

                if (registrationType == IoCRegistrationType.Singleton)
                {
                    foreach (var implementedType in implementing)
                    {
                        AddSingleton(implementedType, type);
                    }
                    AddSingleton(type);
                }
            }
        }

        public void AddTransient<TServiceType>() where TServiceType : class => AddTransient(typeof(TServiceType));

        public void AddTransient(Type serviceType)
        {
            if (!CanRegister)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(AddTransient)}({nameof(Type)}): {CannotResolveErrorMessage}");

            AddTransientInternal(serviceType);
        }

        public abstract void AddTransientInternal(Type serviceType);

        public void AddTransient<TContractType, TServiceType>() where TContractType : class where TServiceType : class => AddTransient(typeof(TContractType), typeof(TServiceType));

        public void AddTransient(Type contractType, Type serviceType)
        {
            if (!CanRegister)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(AddTransient)}({nameof(Type)}, {nameof(Type)}): {CannotResolveErrorMessage}");

            AddTransientInternal(contractType, serviceType);
        }

        public abstract void AddTransientInternal(Type contractType, Type serviceType);

        public void AddSingleton<TServiceType>() where TServiceType : class => AddSingleton(typeof(TServiceType));

        public void AddSingleton<TServiceType>(Func<TServiceType> constructor) where TServiceType : class
        {
            if (!CanRegister)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(AddSingleton)}<{nameof(TServiceType)}>({typeof(Func<TServiceType>)}): {CannotResolveErrorMessage}");

            AddSingletonInternal(constructor);
        }

        public abstract void AddSingletonInternal<TServiceType>(Func<TServiceType> constructor) where TServiceType : class;

        public void AddSingleton(Type serviceType)
        {
            if (!CanRegister)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(AddSingleton)}({nameof(Type)}): {CannotResolveErrorMessage}");

            AddSingletonInternal(serviceType);
        }

        public abstract void AddSingletonInternal(Type serviceType);

        public void AddSingleton<TContractType, TServiceType>() where TContractType : class where TServiceType : class => AddSingleton(typeof(TContractType), typeof(TServiceType));

        public void AddSingleton(Type contractType, Type serviceType)
        {
            if (!CanRegister)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(AddSingleton)}({nameof(Type)}, {nameof(Type)}): {CannotResolveErrorMessage}");

            AddSingletonInternal(contractType, serviceType);
        }

        public abstract void AddSingletonInternal(Type contractType, Type serviceType);

        public TServiceType GetService<TServiceType>() where TServiceType : class
        {
            if (!CanResolve)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(GetService)}<{nameof(TServiceType)}>: {CannotResolveErrorMessage}");

            return GetServiceInternal<TServiceType>();
        }

        public abstract TServiceType GetServiceInternal<TServiceType>() where TServiceType : class;

        public TServiceType GetService<TServiceType>(Type serviceType) where TServiceType : class
        {
            if (!CanResolve)
                throw new InvalidOperationException($"{GetType().FullName}.{nameof(GetService)}<{nameof(TServiceType)}>({nameof(Type)}): {CannotResolveErrorMessage}");

            return GetServiceInternal<TServiceType>(serviceType);
        }

        public abstract TServiceType GetServiceInternal<TServiceType>(Type serviceType) where TServiceType : class;

        public abstract string CannotRegisterErrorMessage { get; }

        public abstract string CannotResolveErrorMessage { get; }
    }

    public enum IoCRegistrationType
    {
        Manual,
        Transient,
        Singleton
    }
}
