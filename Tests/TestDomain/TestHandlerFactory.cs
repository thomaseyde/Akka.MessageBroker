using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Routers.Resolvers;
using Unity;

namespace Tests.TestDomain
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TestHandlerFactory :
        HandlerFactory,
        ForCommand<FirstCommand>,
        ForCommand<SecondCommand>
    {
        public TestHandlerFactory(IUnityContainer container) : base(container) { }

        protected override Props CreateProps()
        {
            var test = Resolve<TestAggregate>();
            return Props.Create(() => new TestHandler(test));
        }
    }
}