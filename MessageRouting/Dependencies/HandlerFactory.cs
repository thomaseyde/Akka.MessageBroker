using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using Microsoft.Practices.Unity;

namespace MessageRouting.Dependencies
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public abstract class HandlerFactory<THandler> : ICreateHandler
    {
        private Props props;
        private readonly IUnityContainer container;

        protected HandlerFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public Props Props => props ?? (props = CreateProps());
        public string Name => Props.Type.FullName;

        protected T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        protected abstract Props CreateProps();
    }
}