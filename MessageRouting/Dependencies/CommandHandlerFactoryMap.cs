using System;
using System.Collections.Generic;
using System.Linq;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;

namespace MessageRouting.Dependencies
{
    public class CommandHandlerFactoryMap
    {
        public static void Register<TAssembly>(IUnityContainer container)
        {
            var commandHandlers = new CommandHandlerFactoryMap();
            commandHandlers.Register<TAssembly>();
            container.RegisterInstance(commandHandlers);
        }

        public CommandHandlerFactoryMap()
        {
            factories = new Dictionary<Type, Type>();
        }

        public void Register<TAssembly>()
        {
            var assembly = typeof(TAssembly).Assembly;

            foreach (var type in assembly.GetTypes().Where(TypeIsHandlerFactory))
            {
                foreach (var specification in CommandSpecifications(type))
                {
                    Map(specification, type);
                }
            }
        }

        public Type FactoryFor(object command)
        {
            return factories.TryGetValue(command.GetType(), out var factory)
                ? factory
                : null;
        }

        private void Map(Type specification, Type type)
        {
            var command = specification.GenericTypeArguments[0];
            factories[command] = type;
        }

        private static IEnumerable<Type> CommandSpecifications(Type type)
        {
            if (type.BaseType == null) return Enumerable.Empty<Type>();

            var handler = type.BaseType.GenericTypeArguments[0];

            return handler.GetInterfaces()
                .Where(t => t.IsGenericType)
                .Where(t => t.GetGenericTypeDefinition() == typeof(ForCommand<>));
        }

        private static bool TypeIsHandlerFactory(Type type)
        {
            if (type.BaseType == null) return false;
            if (!type.BaseType.IsGenericType) return false;
            return type.BaseType.GetGenericTypeDefinition() == typeof(HandlerFactory<>);
        }

        private readonly Dictionary<Type, Type> factories;
    }
}