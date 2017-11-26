using System.Collections.Generic;
using System.Linq;
using MessageBroker.EventSourcing;

namespace Tests
{
    internal static class AggregateLookups
    {
        public static List<TEvent> PublishedEvents<TEvent>(this IRaiseEvents raiser)
        {
            return raiser.GetUncommittedEvents().OfType<TEvent>().ToList();
        }
    }
}