using MessageBroker.EventSourcing;

namespace Tests.TestDomain
{
    public class TestAggregate : Aggregate
    {
        public void DoFirst()
        {
            Emit(new FirstThingHappened());
        }

        public void DoSecond()
        {
            Emit(new SecondThingHappened());
        }

        protected override void Apply(object e) { }
    }
}