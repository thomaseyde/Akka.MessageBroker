using System.Collections.Generic;
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

        public IEnumerable<ICreateHandler> HandlerFactories(object message)
        {
            var commandHandler = CommandHandler(message);

            return commandHandler == null
                ? EventHandlers(message)
                : new[] {commandHandler};
        }

        private IEnumerable<ICreateHandler> EventHandlers(object message)
        {
            var map = container.Resolve<EventHandlerFactoryMap>();

            foreach (var factory in map.FactoriesFor(message))
            {
                yield return (ICreateHandler) container.Resolve(factory);
            }
        }

        private ICreateHandler CommandHandler(object message)
        {
            var map = container.Resolve<CommandHandlerFactoryMap>();
            var handler = map.FactoryFor(message);

            return handler != null 
                ? (ICreateHandler) container.Resolve(handler) 
                : null;
        }
    }
}