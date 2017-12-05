using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;

namespace MessageRouting.Dependencies {
    public class EventHandlerFactoryMap
    {
        public static void Register<TAssembly>(IUnityContainer child)
        {
            var eventHandlers = new EventHandlerFactoryMap();
            eventHandlers.Register<TAssembly>();
            child.RegisterInstance(eventHandlers);
        }

        public EventHandlerFactoryMap()
        {
            factories = new Dictionary<Type, HashSet<Type>>();
        }

        public void Register<TAssembly>()
        {
            var assembly = typeof(TAssembly).Assembly;

            foreach (var factory in HandlerFactoryTypes(assembly))
            {
                foreach (var specification in EventSpecifications(factory))
                {
                    Map(specification, factory);
                }
            }
        }

        private void Map(Type specification, Type factory)
        {
            var @event = EventType(specification);

            if (!factories.ContainsKey(@event))
            {
                factories[@event] = new HashSet<Type>();
            }

            factories[@event].Add(factory);
        }

        private static Type EventType(Type specification)
        {
            return specification.GenericTypeArguments[0];
        }

        private static IEnumerable<Type> HandlerFactoryTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(TypeIsHandlerFactory);
        }

        public IEnumerable<Type> FactoriesFor(object @event)
        {
            return factories.TryGetValue(@event.GetType(), out var handlers) 
                ? handlers 
                : Enumerable.Empty<Type>();
        }

        private static IEnumerable<Type> EventSpecifications(Type type)
        {
            var baseType = type.BaseType;

            if (baseType == null) return Enumerable.Empty<Type>();

            return baseType
                .GenericTypeArguments[0]
                .GetInterfaces()
                .Where(t => t.IsGenericType)
                .Where(t => t.GetGenericTypeDefinition() == typeof(ForEvent<>));
        }

        private static bool TypeIsHandlerFactory(Type type)
        {
            if (type.BaseType == null) return false;
            if (!type.BaseType.IsGenericType) return false;
            return type.BaseType.GetGenericTypeDefinition() == typeof(HandlerFactory<>);
        }

        private readonly Dictionary<Type, HashSet<Type>> factories;
    }
}