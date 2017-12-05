using Akka.Actor;
using MessageRouting.Routers;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Loggers
{
    public class TestLoggerHandler : ReceiveActor,
        ForEvent<ThirdThingHappened>
    {
        public TestLoggerHandler(TestLogger log)
        {
            Receive<ThirdThingHappened>(c => log.CompleteThird());
        }
    }
}