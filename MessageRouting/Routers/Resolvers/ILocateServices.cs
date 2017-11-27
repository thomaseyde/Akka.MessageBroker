using System;

namespace MessageRouting.Routers.Resolvers
{
    public interface ILocateServices
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}