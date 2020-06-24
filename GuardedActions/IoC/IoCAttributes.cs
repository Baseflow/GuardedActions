using System;

namespace GuardedActions.IoC
{
    public class GuardedActionsIoCRegistrationAttribute : Attribute
    {
        public GuardedActionsIoCRegistrationAttribute(IoCRegistrationType registrationType = IoCRegistrationType.Transient)
        {
            RegistrationType = registrationType;
        }

        public IoCRegistrationType RegistrationType { get; }
    }

    public enum IoCRegistrationType
    {
        Manual,
        Transient,
        Singleton
    }
}
