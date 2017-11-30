using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Akka.Actor;
using MessageRouting.Routers.Logging;
using MessageRouting.Routers.Resolvers;
using Microsoft.Practices.Unity;

namespace MessageRouting.Routers
{
    public class EventRouter : ReceiveActor
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public EventRouter(IUnityContainer container)
        {
            var log = container.Resolve<ILogMessages>();

            Receive<object>(message =>
            {
                var handler = ResolveHandler(container, message);
                handler.Tell(message);
                log.Message(message);
            });
        }

        private static IActorRef ResolveHandler(IUnityContainer container, object message)
        {
            var factory = container.ResolveEventHandlerFactory(message);
            var handler = Context.Child(factory.Name);

            return handler.Equals(Nobody.Instance)
                ? Context.ActorOf(factory.Props, factory.Name)
                : handler;
        }

        public static Props Create(IUnityContainer container)
        {
            var name = Assembly.GetCallingAssembly().FullName;
            var child = container.Resolve<IUnityContainer>(name);
            return Props.Create(() => new EventRouter(child));
        }
    }
}