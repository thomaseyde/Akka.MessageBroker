namespace MessageBroker.Messaging.Commands
{
    public static class FactoryResolvers
    {
        public static CommandHandlerFactory ResolveCommandHandlerFactory(this ILocateServices services, object message)
        {
            var command = message.GetType();
            var handler = typeof(IHandleCommand<>).MakeGenericType(command);
            return (CommandHandlerFactory) services.Resolve(handler);
        }
    }
}