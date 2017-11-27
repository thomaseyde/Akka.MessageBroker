using Akka.Actor;

namespace MessageRouting.Routers.Resolvers
{
    public abstract class HandlerFactory
    {
        private Props props;
        private readonly ILocateServices services;

        protected HandlerFactory(ILocateServices services)
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