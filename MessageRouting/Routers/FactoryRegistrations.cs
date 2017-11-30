using System;
using System.Collections.Generic;
using System.Linq;
using MessageRouting.Routers.Resolvers;
using Microsoft.Practices.Unity;

namespace MessageRouting.Routers
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
                var commandHandler = type.GetInterfaces(typeof(ForCommand<>));
                foreach (var handler in commandHandler)
                {
                    child.RegisterType(handler, type);
                }
                var eventHandler = type.GetInterfaces(typeof(ForEvent<>));
                foreach (var handler in eventHandler)
                {
                    child.RegisterType(handler, type);
                }
            }

            if (!container.IsRegistered(typeof(ILogMessages)))
            {
                container.RegisterType<ILogMessages, NoLogging>();
            }

            return child;
        }

        private static IEnumerable<Type> GetInterfaces(this Type type, Type specification)
        {
            return type.GetInterfaces()
                       .Where(t => t.IsGenericType)
                       .Where(t => t.GetGenericTypeDefinition() == specification);
        }
    }
}