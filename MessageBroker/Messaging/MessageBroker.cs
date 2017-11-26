using Akka.Actor;
using MessageBroker.Messaging.Commands;

namespace MessageBroker.Messaging
{
    public class MessageBroker : ReceiveActor
    {
        public MessageBroker(ILocateServices services)
        {
            Receive<object>(message => ResolveHandler(services, message).Tell(message));
        }

        private static IActorRef ResolveHandler(ILocateServices services, object message)
        {
            var factory = services.ResolveCommandHandlerFactory(message);
            var handler = Context.Child(factory.Name);

            return handler.Equals(Nobody.Instance)
                ? Context.ActorOf(factory.Props, factory.Name)
                : handler;
        }
    }
}