using System;
using System.Collections.Generic;
using System.Linq;
using MessageRouting.Logging;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;

namespace MessageRouting.Dependencies
{
    public static class FactoryRegistrations
    {
        public static IUnityContainer RegisterHandlerFactoriesInAssembly<TAssembly>(this IUnityContainer container)
        {
            var assembly = typeof(TAssembly).Assembly;
            var name = assembly.FullName;
            container.RegisterInstance(typeof(IUnityContainer), name, container.CreateChildContainer());
            var child = container.Resolve<IUnityContainer>(name);

            foreach (var type in assembly.GetTypes())
            {
                RegisterCommandHandlerFactories(type, child);
                RegisterEventHandlerFactories(type, child);
            }

            if (!container.IsRegistered(typeof(ILogMessages)))
            {
                container.RegisterType<ILogMessages, NoLogging>();
            }

            return child;
        }

        private static void RegisterCommandHandlerFactories(Type type, IUnityContainer child)
        {
            var commandHandlers = type.GetInterfaces(typeof(ForCommand<>));
            foreach (var handler in commandHandlers)
            {
                child.RegisterType(handler, type);
            }
        }

        private static void RegisterEventHandlerFactories(Type type, IUnityContainer child)
        {
            var eventHandlers = type.GetInterfaces(typeof(ForEvent<>));
            foreach (var handler in eventHandlers)
            {
                child.RegisterType(handler, type, Guid.NewGuid().ToString());
            }
        }

        private static IEnumerable<Type> GetInterfaces(this Type type, Type specification)
        {
            return type.GetInterfaces()
                .Where(t => t.IsGenericType)
                .Where(t => t.GetGenericTypeDefinition() == specification);
        }
    }
}