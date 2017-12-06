using MessageRouting.Routers;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Tests
{
    public class TestHandler : HandlerActor,
        ForCommand<FirstCommand>,
        ForCommand<SecondCommand>,
        ForEvent<FirstThingHappened>,
        ForEvent<SecondThingHappened>,
        ForEvent<ThirdThingHappened>
    {
        private readonly TestAggregate test;

        public TestHandler(TestAggregate test) => this.test = test;

        public void Apply(FirstThingHappened e) => test.CompleteFirst();
        public void Apply(SecondThingHappened e) => test.CompleteSecond();
        public void Apply(ThirdThingHappened e) => test.CompleteThird();

        public void Handle(FirstCommand c) => test.DoFirst();
        public void Handle(SecondCommand c) => test.DoSecond();
    }
}