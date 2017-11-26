using System;
using System.Diagnostics.CodeAnalysis;
using MessageBroker.Messaging.Commands;
using Unity;

namespace MessageBroker.Messaging.Unity
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class UnityServiceLocator : ILocateServices
    {
        private readonly IUnityContainer container;

        public UnityServiceLocator(IUnityContainer container)
        {
            this.container = container;
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        public void RegisterType(Type service, Type implementation) { }
    }
}