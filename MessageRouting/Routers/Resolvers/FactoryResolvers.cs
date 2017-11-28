using Microsoft.Practices.Unity;

namespace MessageRouting.Routers.Resolvers
{
    public static class FactoryResolvers
    {
        public static HandlerFactory ResolveCommandHandlerFactory(this IUnityContainer container, object message)
        {
            var command = message.GetType();
            var handler = typeof(ForCommand<>).MakeGenericType(command);
            return (HandlerFactory) container.Resolve(handler);
        }

        public static HandlerFactory ResolveEventHandlerFactory(this IUnityContainer container, object message)
        {
            var command = message.GetType();
            var handler = typeof(ForEvent<>).MakeGenericType(command);
            return (HandlerFactory) container.Resolve(handler);
        }
    }
}