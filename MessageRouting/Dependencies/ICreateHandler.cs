using Akka.Actor;

namespace MessageRouting.Dependencies
{
    public interface ICreateHandler
    {
        string Name { get; }
        Props Props { get; }
    }
}