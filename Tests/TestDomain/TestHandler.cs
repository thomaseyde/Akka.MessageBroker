using Akka.Actor;

namespace Tests.TestDomain
{
    public class TestHandler : ReceiveActor
    {
        public TestHandler(TestAggregate test)
        {
            Receive<FirstCommand>(c => test.DoFirst());
            Receive<SecondCommand>(c => test.DoSecond());
        }
    }
}