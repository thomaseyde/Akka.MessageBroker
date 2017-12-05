using System.Diagnostics.CodeAnalysis;
using Akka.Actor;
using MessageRouting.Dependencies;
using Microsoft.Practices.Unity;

namespace Tests.TestDomain.Tests
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TestHandlerFactory : HandlerFactory<TestHandler>
    {
        public TestHandlerFactory(IUnityContainer container) : base(container) { }

        protected override Props CreateProps()
        {
            var test = Resolve<TestAggregate>();
            return Props.Create(() => new TestHandler(test));
        }
    }
}