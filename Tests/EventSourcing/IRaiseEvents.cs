using System.Collections;

namespace Tests.EventSourcing
{
    public interface IRaiseEvents
    {
        IEnumerable GetUncommittedEvents();
        void CommitEvents();
    }
}