using System;
using System.Collections.Generic;

namespace MessageBroker.EventSourcing
{
    public interface IRestoreAggregate
    {
        void LoadFromHistory(Guid id, IEnumerable<object> events);
    }
}