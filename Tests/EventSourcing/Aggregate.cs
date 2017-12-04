using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Tests.EventSourcing
{
    public abstract class Aggregate : IRaiseEvents
    {
        private readonly List<object> uncommittedEvents = new List<object>();

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected abstract void Apply(object e);

        protected void Emit(object e)
        {
            uncommittedEvents.Add(e);
            Apply(e);
        }

        public IEnumerable GetUncommittedEvents()
        {
            return uncommittedEvents;
        }
    }
}