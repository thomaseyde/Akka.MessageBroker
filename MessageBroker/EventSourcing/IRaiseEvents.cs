using System.Collections;

namespace MessageBroker.EventSourcing
{
    public interface IRaiseEvents
    {
        IEnumerable GetUncommittedEvents();
        void CommitEvents();
    }
}