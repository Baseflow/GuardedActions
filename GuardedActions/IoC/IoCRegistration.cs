using System;
using System.Collections.Generic;
using System.Linq;
using GuardedActions.Commands.Interfaces;
using GuardedActions.ExceptionHandlers;
using GuardedActions.ExceptionHandlers.Interfaces;
using GuardedActions.Extensions;
using GuardedActions.Interfaces;
using GuardedActions.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace GuardedActions.IoC
{
    public class IoCRegistration
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<Type> _createableTypes;

        public IoCRegistration(IServiceCollection services, params string[] assemblyNames)
        {
            _services = services;
            _createableTypes = AssemblyUtils.GetRegistrationAssemblies(assemblyNames).GetCreatableTypes();
        }

        public void Register()
        {
            AddSweep<IAction>(IoCRegistrationType.Transient);

            AddSweep<ICommandBuilder>(IoCRegistrationType.Transient);

            var exceptionHandlerTypes = AddSweep<IExceptionHandler>(IoCRegistrationType.Singleton);

            _services.AddSingleton<IExceptionHandlingActionFactory, ExceptionHandlingActionFactory>();

            _services.AddSingleton<IExceptionGuard>(serviceProvider =>
            {
                var factory = serviceProvider.GetService<IExceptionHandlingActionFactory>();
                var handlers = exceptionHandlerTypes.Select(handlerType => serviceProvider.GetService<IExceptionHandler>(handlerType)).ToList();
                return new ExceptionGuard(handlers, factory);
            });
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
                var attribute = type.GetAttribute<GuardedActionsIoCRegistrationAttribute>();

                registrationType = attribute != null ? attribute.RegistrationType : registrationType;

                if (registrationType == IoCRegistrationType.Manual)
                    continue;

                var implementing = type.GetImplementedInterfaces(true);

                if (registrationType == IoCRegistrationType.Transient)
                {
                    foreach (var implementedType in implementing)
                    {
                        _services.AddTransient(implementedType, type);
                    }
                    _services.AddTransient(type);
                    continue;
                }

                if (registrationType == IoCRegistrationType.Singleton)
                {
                    foreach (var implementedType in implementing)
                    {
                        // TODO Ask Artur what the AutoActivate is for
                        //if (singleInstanceType.AutoActivate)
                        //{
                        //    IocProvider.RegisterSingleton(implementedType, IocProvider.IoCConstruct(type));
                        //}
                        //else
                        //{
                        //    IocProvider.RegisterSingleton(implementedType, () => IocProvider.IoCConstruct(type));
                        //}
                        _services.AddSingleton(implementedType, type);
                    }
                    _services.AddSingleton(type);
                }
            }
        }
    }
}
