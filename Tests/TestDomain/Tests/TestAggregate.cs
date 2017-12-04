using System.Collections.Generic;
using Tests.EventSourcing;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Tests
{
    public class TestAggregate : Aggregate
    {
        private List<string> currentLog;

        public void DoFirst()
        {
            Emit(new FirstThingHappened());
        }

        public void DoSecond()
        {
            Emit(new SecondThingHappened());
        }

        public void CompleteFirst()
        {
            currentLog.Add("first");
        }

        public void CompleteSecond()
        {
            currentLog.Add("second");
        }

        public void CompleteThird()
        {
            currentLog.Add("third command");
        }

        protected override void Apply(object e) { }

        public TestAggregate WithLog(List<string> log)
        {
            currentLog = log;
            return this;
        }
    }
}