using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Logging;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;
using Tests.TestDomain.Events;

namespace Tests.TestDomain.Tests
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TestHandlerFactory :
        HandlerFactory,
        ForCommand<FirstCommand>,
        ForCommand<SecondCommand>,
        ForEvent<FirstThingHappened>,
        ForEvent<SecondThingHappened>,
        ForEvent<ThirdThingHappened>
    {
        public TestHandlerFactory(IUnityContainer container) : base(container) { }

        protected override Props CreateProps()
        {
            var test = Resolve<TestAggregate>();
            return Props.Create(() => new TestHandler(test));
        }
    }
}