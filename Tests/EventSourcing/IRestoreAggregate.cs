using System;
using System.Collections.Generic;

namespace Tests.EventSourcing
{
    public interface IRestoreAggregate
    {
        void LoadFromHistory(Guid id, IEnumerable<object> events);
    }
}