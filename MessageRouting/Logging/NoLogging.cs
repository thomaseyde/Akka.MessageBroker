namespace MessageRouting.Logging
{
    internal class NoLogging : ILogMessages
    {
        public void Message(object message) { }
    }
}