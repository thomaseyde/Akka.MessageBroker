using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Routers.Resolvers;

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
            return Props.Create(() => new EventRouter(services));
        }
    }
}