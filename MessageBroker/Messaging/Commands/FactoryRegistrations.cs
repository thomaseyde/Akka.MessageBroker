using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace MessageBroker.Messaging.Commands
{
    public static class FactoryRegistrations
    {
        public static IUnityContainer RegisterCommandHandlerFactories<TAssembly>(this IUnityContainer container)
        {
            foreach (var type in typeof(TAssembly).Assembly.GetTypes())
            {
                var services = type.GetInterfaces(typeof(IHandleCommand<>));
                foreach (var service in services)
                {
                    container.RegisterType(service, type);
                }
            }
            return container;
        }

        private static IEnumerable<Type> GetInterfaces(this Type type, Type specification)
        {
            return type.GetInterfaces()
                .Where(t => t.IsGenericType)
                .Where(t => t.GetGenericTypeDefinition() == specification);
        }
    }
}