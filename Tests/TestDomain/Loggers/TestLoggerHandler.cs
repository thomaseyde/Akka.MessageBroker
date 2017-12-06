using MessageRouting.Routers;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Loggers
{
    public class TestLoggerHandler : HandlerActor,
        ForEvent<ThirdThingHappened>
    {
        private readonly TestLogger log;

        public TestLoggerHandler(TestLogger log)
        {
            this.log = log;
        }

        public void Apply(ThirdThingHappened e)
        {
            log.CompleteThird();
        }
    }
}