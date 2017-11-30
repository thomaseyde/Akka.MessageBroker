using System.Diagnostics.CodeAnalysis;

namespace MessageRouting.Routers.Logging {
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class NoLogging : ILogMessages
    {
        public void Message(object message) { }
    }
}