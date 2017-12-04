using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Logging;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Loggers
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TestLoggerHandlerFactory :
        HandlerFactory,
        ForEvent<ThirdThingHappened>
    {
        public TestLoggerHandlerFactory(IUnityContainer container) : base(container) { }

        protected override Props CreateProps()
        {
            var log = Resolve<TestLogger>();
            return Props.Create(() => new TestLoggerHandler(log));
        }
    }
}