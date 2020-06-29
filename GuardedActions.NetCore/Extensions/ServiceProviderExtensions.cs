using System;

namespace GuardedActions.NetCore.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static TServiceTypeBase GetService<TServiceTypeBase>(this IServiceProvider? serviceProvider, Type serviceType) where TServiceTypeBase : class =>
            serviceProvider?.GetService(serviceType) as TServiceTypeBase;
    }
}
