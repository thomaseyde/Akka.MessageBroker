using System.Collections.Generic;
using Akka.Actor;
using MessageRouting.Dependencies;
using MessageRouting.Routers;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Tests.TestDomain.Events;
using Tests.TestDomain.Loggers;
using Tests.TestDomain.Tests;

namespace Tests
{
    [TestFixture]
    public class Verify_event_handler_creation
    {
        [Test]
        public void Single_event()
        {
            var container = new UnityContainer();
            var log = new List<string>();
            var test = new TestAggregate().WithLog(log);
            container
                .RegisterInstance(test)
                .RegisterHandlerFactoriesInAssembly<TestAggregate>();

            var system = ActorSystem.Create("test");
            var router = system.ActorOf(MessageRouter.Create<TestAggregate>(container), "router");

            router.Tell(new FirstThingHappened());

            Assert.That(() => log, Contains.Item("first").After(500, 50));
        }

        [Test]
        public void Multiple_event()
        {
            var container = new UnityContainer();
            var log = new List<string>();
            var test = new TestAggregate().WithLog(log);
            container
                .RegisterInstance(test)
                .RegisterHandlerFactoriesInAssembly<TestAggregate>();

            var system = ActorSystem.Create("test");
            var router = system.ActorOf(MessageRouter.Create<TestAggregate>(container), "router");

            router.Tell(new FirstThingHappened());
            router.Tell(new SecondThingHappened());

            Assert.That(() => log, Contains.Item("first").After(500, 50));
            Assert.That(() => log, Contains.Item("second").After(500, 50));
        }

        [Test]
        public void Multiple_handlers()
        {
            var container = new UnityContainer();
            var log = new List<string>();
            var test = new TestAggregate().WithLog(log);
            container
                .RegisterInstance(new TestLogger(log))
                .RegisterInstance(test)
                .RegisterHandlerFactoriesInAssembly<TestAggregate>();

            var system = ActorSystem.Create("test");
            var router = system.ActorOf(MessageRouter.Create<TestAggregate>(container), "router");

            router.Tell(new FirstThingHappened());
            router.Tell(new SecondThingHappened());
            router.Tell(new ThirdThingHappened());

            Assert.That(() => log, Contains.Item("first").After(500, 50));
            Assert.That(() => log, Contains.Item("second").After(500, 50));
            Assert.That(() => log, Contains.Item("third command").After(500, 50));
            Assert.That(() => log, Contains.Item("third event").After(500, 50));
        }
    }
}