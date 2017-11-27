namespace MessageRouting.Routers.Resolvers
{
    public static class FactoryResolvers
    {
        public static HandlerFactory ResolveCommandHandlerFactory(this ILocateServices services, object message)
        {
            var command = message.GetType();
            var handler = typeof(ForCommand<>).MakeGenericType(command);
            return (HandlerFactory) services.Resolve(handler);
        }

        public static HandlerFactory ResolveEventHandlerFactory(this ILocateServices services, object message)
        {
            var command = message.GetType();
            var handler = typeof(ForEvent<>).MakeGenericType(command);
            return (HandlerFactory) services.Resolve(handler);
        }
    }
}