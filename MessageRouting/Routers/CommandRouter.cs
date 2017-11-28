using Akka.Actor;
using MessageRouting.Routers.Resolvers;

namespace MessageRouting.Routers
{
    public class CommandRouter : ReceiveActor
    {
        public CommandRouter(IUnityContainer container)
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
            var factory = container.ResolveCommandHandlerFactory(message);
            var handler = Context.Child(factory.Name);

            return handler.Equals(Nobody.Instance)
                ? Context.ActorOf(factory.Props, factory.Name)
                : handler;
        }

        public static Props Create(IUnityContainer container)
        {
            return Props.Create(() => new CommandRouter(services));
        }
    }

    public interface ILogMessages
    {
        void Message(object message);
    }

    internal class NoLogging : ILogMessages
    {
        public void Message(object message) { }
    }
}