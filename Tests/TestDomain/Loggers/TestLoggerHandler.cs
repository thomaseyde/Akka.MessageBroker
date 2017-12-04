using Akka.Actor;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Loggers
{
    public class TestLoggerHandler : ReceiveActor
    {
        public TestLoggerHandler(TestLogger log)
        {
            Receive<ThirdThingHappened>(c => log.CompleteThird());
        }
    }
}