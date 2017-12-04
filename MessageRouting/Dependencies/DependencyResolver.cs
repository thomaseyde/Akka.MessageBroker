using System;
using System.Collections.Generic;
using MessageRouting.Logging;
using Unity;

namespace MessageRouting.Dependencies
{
    public class DependencyResolver
    {
        private readonly IUnityContainer container;
        private readonly FactoryResolver factoryResolver;

        private DependencyResolver(IUnityContainer container, Type fromAssembly)
        {
            var name = fromAssembly.Assembly.FullName;
            this.container = container.Resolve<IUnityContainer>(name);
            factoryResolver = new FactoryResolver(this.container);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public IEnumerable<HandlerFactory> ResolveHandlerFactories(object message)
        {
            return factoryResolver.HandlerFactories(message);
        }

        public static DependencyResolver Create<TAssembly>(IUnityContainer container)
        {
            return new DependencyResolver(container, typeof(TAssembly));
        }
    }
}