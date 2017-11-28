using System;
using System.Collections.Generic;
using System.Linq;
using MessageRouting.Routers.Resolvers;
using Microsoft.Practices.Unity;
using UnityServiceLocator = MessageRouting.Routers.Unity.UnityServiceLocator;

namespace MessageRouting.Routers
{
    public static class FactoryRegistrations
    {
        public static IUnityContainer RegisterHandlerFactories<TAssembly>(this IUnityContainer container)
        {
            foreach (var type in typeof(TAssembly).Assembly.GetTypes())
            {
                var commandHandler = type.GetInterfaces(typeof(ForCommand<>));
                foreach (var handler in commandHandler)
                {
                    container.RegisterType(handler, type);
                }
                var eventHandler = type.GetInterfaces(typeof(ForEvent<>));
                foreach (var handler in eventHandler)
                {
                    container.RegisterType(handler, type);
                }
            }

            container.RegisterType<ILocateServices, UnityServiceLocator>();
            container.RegisterType<ILogMessages, NoLogging>();
            
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