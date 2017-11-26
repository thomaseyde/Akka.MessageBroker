using System;

namespace MessageBroker.Messaging.Commands
{
    public interface ILocateServices
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}