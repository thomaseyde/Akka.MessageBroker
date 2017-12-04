using Akka.Actor;
using MessageRouting.Dependencies;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Tests.TestDomain.Events;
using Tests.TestDomain.Tests;

namespace Tests
{
    [TestFixture]
    public class Verify_command_handler_creation
    {
        [Test]
        public void Single_command()
        {
            var container = new UnityContainer();
            var test = new TestAggregate();
            container
                .RegisterInstance(test)
                .RegisterHandlerFactoriesInAssembly<TestAggregate>();

            var system = ActorSystem.Create("test");
            var router = system.ActorOf(MessageRouter.Create<TestAggregate>(container),"router");

            router.Tell(new FirstCommand());
            router.Tell(new FirstCommand());

            Assert.That(() => test.PublishedEvents<FirstThingHappened>(), Has.Count.EqualTo(2).After(500, 50));
        }

        [Test]
        public void Multiple_commands()
        {
            var container = new UnityContainer();
            var test = new TestAggregate();
            container
                .RegisterHandlerFactoriesInAssembly<TestAggregate>()
                .RegisterInstance(test);

            var system = ActorSystem.Create("test");
            var broker = system.ActorOf(MessageRouter.Create<TestAggregate>(container), "router");

            broker.Tell(new FirstCommand());
            broker.Tell(new SecondCommand());

            Assert.That(() => test.PublishedEvents<FirstThingHappened>(), Has.Count.EqualTo(1).After(500, 50));
            Assert.That(() => test.PublishedEvents<SecondThingHappened>(), Has.Count.EqualTo(1).After(500, 50));
        }
    }
}