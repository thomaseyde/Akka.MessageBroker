using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Dependencies;
using Microsoft.Practices.Unity;

namespace Tests.TestDomain.Loggers
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TestLoggerHandlerFactory :
        HandlerFactory<TestLoggerHandler>
    {
        public TestLoggerHandlerFactory(IUnityContainer container) : base(container) { }

        protected override Props CreateProps()
        {
            var log = Resolve<TestLogger>();
            return Props.Create(() => new TestLoggerHandler(log));
        }
    }
}