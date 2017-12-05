using MessageRouting.Dependencies;
using NUnit.Framework;
using Tests.TestDomain.Events;
using Tests.TestDomain.Loggers;
using Tests.TestDomain.Tests;

namespace Tests
{

    [TestFixture]
    public class Map_handler_factory_types
    {
        [Test]
        public void For_command_handlers()
        {
            var map = new CommandHandlerFactoryMap();
            map.Register<TestAggregate>();

            Assert.That(map.FactoryFor(new FirstCommand()), Is.EqualTo(typeof(TestHandlerFactory)));
        }

        [Test]
        public void For_event_handlers()
        {
            var map = new EventHandlerFactoryMap();
            map.Register<TestAggregate>();

            Assert.That(map.FactoriesFor(new FirstThingHappened()), Contains.Item(typeof(TestHandlerFactory)));
            Assert.That(map.FactoriesFor(new SecondThingHappened()), Contains.Item(typeof(TestHandlerFactory)));
            Assert.That(map.FactoriesFor(new ThirdThingHappened()), Contains.Item(typeof(TestHandlerFactory)));
            Assert.That(map.FactoriesFor(new ThirdThingHappened()), Contains.Item(typeof(TestLoggerHandlerFactory)));

        }

    }
}