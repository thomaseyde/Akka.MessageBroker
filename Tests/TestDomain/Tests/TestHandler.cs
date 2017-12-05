using Akka.Actor;
using MessageRouting.Routers;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Tests
{
    public class TestHandler : ReceiveActor,
        ForCommand<FirstCommand>,
        ForCommand<SecondCommand>,
        ForEvent<FirstThingHappened>,
        ForEvent<SecondThingHappened>,
        ForEvent<ThirdThingHappened>
    {
        public TestHandler(TestAggregate test)
        {
            Receive<FirstCommand>(c => test.DoFirst());
            Receive<SecondCommand>(c => test.DoSecond());
            Receive<FirstThingHappened>(c => test.CompleteFirst());
            Receive<SecondThingHappened>(c => test.CompleteSecond());
            Receive<ThirdThingHappened>(c => test.CompleteThird());
        }
    }
}