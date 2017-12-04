using System.Collections.Generic;
using System.Linq;
using MessageRouting.Logging;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;

namespace MessageRouting.Dependencies
{
    internal class FactoryResolver
    {
        private readonly IUnityContainer container;

        public FactoryResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public IEnumerable<HandlerFactory> HandlerFactories(object message)
        {
            var commandHandler = CommandHandler(message);

            return commandHandler == null
                ? EventHandlers(message)
                : new[] {commandHandler};
        }

        private IEnumerable<HandlerFactory> EventHandlers(object message)
        {
            var @event = message.GetType();
            var handler = typeof(ForEvent<>).MakeGenericType(@event);

            var handlers = container.ResolveAll(handler);

            return handlers.OfType<HandlerFactory>();
        }

        private HandlerFactory CommandHandler(object message)
        {
            var command = message.GetType();
            var handler = typeof(ForCommand<>).MakeGenericType(command);

            if (container.IsRegistered(handler))
            {
                return (HandlerFactory) container.Resolve(handler);
            }

            return null;
        }
    }
}