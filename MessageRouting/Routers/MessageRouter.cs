using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Dependencies;
using MessageRouting.Logging;
using Unity;

namespace MessageRouting.Routers
{
    public class MessageRouter : ReceiveActor
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public MessageRouter(DependencyResolver dependencies)
        {
            var log = dependencies.Resolve<ILogMessages>();

            Receive<object>(message =>
            {
                var handlers = ResolveHandlers(dependencies, message);

                foreach (var handler in handlers)
                {
                    handler.Tell(message);
                }

                log.Message(message);
            });
        }

        private static IEnumerable<IActorRef> ResolveHandlers(DependencyResolver dependencies, object message)
        {
            var factories = dependencies.ResolveHandlerFactories(message);

            foreach (var factory in factories)
            {
                var handler = Context.Child(factory.Name);

                yield return handler.Equals(Nobody.Instance)
                    ? Context.ActorOf(factory.Props, factory.Name)
                    : handler;
            }
        }

        public static Props Create<TAssembly>(IUnityContainer container)
        {
            var dependencies = DependencyResolver.Create<TAssembly>(container);
            return Props.Create(() => new MessageRouter(dependencies));
        }
    }
}