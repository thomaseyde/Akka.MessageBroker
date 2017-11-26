using Akka.Actor;

namespace MessageBroker.Messaging.Commands
{
    public abstract class CommandHandlerFactory
    {
        private Props props;
        private readonly ILocateServices services;

        protected CommandHandlerFactory(ILocateServices services)
        {
            this.services = services;
        }

        public Props Props => props ?? (props = CreateProps());
        public string Name => Props.Type.FullName;

        protected T Resolve<T>()
        {
            return services.Resolve<T>();
        }

        protected abstract Props CreateProps();
    }
}