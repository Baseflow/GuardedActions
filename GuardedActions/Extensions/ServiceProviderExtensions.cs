using System;

namespace GuardedActions.Extensions
{
    // TODO: Seperate NuGet?
    public static class ServiceProviderExtensions
    {
        public static TServiceTypeBase GetService<TServiceTypeBase>(this IServiceProvider serviceProvider, Type serviceType) where TServiceTypeBase : class =>
            serviceProvider?.GetService(serviceType) as TServiceTypeBase;
    }
}
