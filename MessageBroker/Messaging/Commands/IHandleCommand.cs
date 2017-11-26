using System.Diagnostics.CodeAnalysis;

namespace MessageBroker.Messaging.Commands
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public interface IHandleCommand<TCommand> { }
}