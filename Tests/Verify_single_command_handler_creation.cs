using Akka.Actor;
using MessageRouting.Routers;
using NUnit.Framework;
using Tests.TestDomain;
using Unity;

namespace Tests
{
    [TestFixture]
    public class Verify_single_command_handler_creation
    {
        [Test]
        public void Single_command()
        {
            var container = new UnityContainer();
            var test = new TestAggregate();
            container
                .RegisterHandlerFactories<TestAggregate>()
                .RegisterInstance(test);

            var system = ActorSystem.Create("test");
            var router = system.ActorOf(Props.Create(() => new CommandRouter(container.Resolve<ILocateServices>())),"router");

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
                .RegisterHandlerFactories<TestAggregate>()
                .RegisterInstance(test);

            var system = ActorSystem.Create("test");
            var broker = system.ActorOf(Props.Create(() => new CommandRouter(container.Resolve<ILocateServices>())),
                "broker");

            broker.Tell(new FirstCommand());
            broker.Tell(new SecondCommand());

            Assert.That(() => test.PublishedEvents<FirstThingHappened>(), Has.Count.EqualTo(1).After(500, 50));
            Assert.That(() => test.PublishedEvents<SecondThingHappened>(), Has.Count.EqualTo(1).After(500, 50));
        }
    }
}