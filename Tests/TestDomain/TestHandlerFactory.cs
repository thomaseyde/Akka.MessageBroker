using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Routers.Resolvers;

namespace Tests.TestDomain
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TestHandlerFactory :
        HandlerFactory,
        ForCommand<FirstCommand>,
        ForCommand<SecondCommand>
    {
        public TestHandlerFactory(ILocateServices services) : base(services) { }

        protected override Props CreateProps()
        {
            var test = Resolve<TestAggregate>();
            return Props.Create(() => new TestHandler(test));
        }
    }
}