using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MessageBroker.EventSourcing
{
    public abstract class Aggregate : IRestoreAggregate, IRaiseEvents
    {
        private readonly List<object> uncommittedEvents = new List<object>();
        public Guid Id { get; private set; }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected abstract void Apply(object e);

        public void LoadFromHistory(Guid id, IEnumerable<object> events)
        {
            Id = id;
            foreach (var e in events)
            {
                Apply(e);
            }
        }

        protected void Emit(object e)
        {
            uncommittedEvents.Add(e);
            Apply(e);
        }

        public IEnumerable GetUncommittedEvents()
        {
            return uncommittedEvents;
        }

        void IRaiseEvents.CommitEvents()
        {
            uncommittedEvents.Clear();
        }
    }
}