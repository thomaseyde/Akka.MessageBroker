using Unity;

namespace MessageRouting.Routers.Resolvers
{
    public static class FactoryResolvers
    {
        public static HandlerFactory ResolveCommandHandlerFactory(this IUnityContainer container, object message)
        {
            var command = message.GetType();
            var handler = typeof(ForCommand<>).MakeGenericType(command);
            var child = container.Resolve<IUnityContainer>(message.GetType().Assembly.FullName);
            return (HandlerFactory) child.Resolve(handler);
        }

        public static HandlerFactory ResolveEventHandlerFactory(this IUnityContainer container, object message)
        {
            var command = message.GetType();
            var handler = typeof(ForEvent<>).MakeGenericType(command);
            var child = container.Resolve<IUnityContainer>(message.GetType().Assembly.FullName);
            return (HandlerFactory) child.Resolve(handler);
        }
    }
}