using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Routers.Resolvers;

namespace MessageRouting.Routers
{
    public class EventRouter : ReceiveActor
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public EventRouter(ILocateServices services)
        {
            var log = services.Resolve<ILogMessages>();

            Receive<object>(message =>
            {
                var handler = ResolveHandler(services, message);
                handler.Tell(message);
                log.Message(message);
            });
        }

        private static IActorRef ResolveHandler(ILocateServices services, object message)
        {
            var factory = services.ResolveEventHandlerFactory(message);
            var handler = Context.Child(factory.Name);

            return handler.Equals(Nobody.Instance)
                ? Context.ActorOf(factory.Props, factory.Name)
                : handler;
        }

        public static Props Create(ILocateServices services)
        {
            return Props.Create(() => new EventRouter(services));
        }
    }
}