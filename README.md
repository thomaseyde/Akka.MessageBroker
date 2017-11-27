# Akka.MessageBroker

A proof of concept of a self registering message broker for command handlers in applications built with Akka.NET.

Implement a command handler:

    public class ReminderIndexHandler : ReceiveActor
    {
        public ReminderIndexHandler(ReminderIndex reminders)
        {
            Receive<SendRegistrationReminders>(command => reminders.Send());
        }
    }
	
Implement a command handler factory:

    public class ReminderIndexHandlerFactory :
        CommandHandlerFactory,
        IHandleCommand<SendReminderRegistrations>
    {
        public ReminderIndexHandlerFactory(ILocateServices services) : base(services) { }

        protected override Props CreateProps()
        {
            var reminders = Resolve<ReminderIndex>();
            return Props.Create(() => new ReminderIndexHandler(reminders));
        }
    }

Register factories:

    var container = new UnityContainer();
    container.RegisterCommandHandlerFactories<ReminderIndex>();
	
Send a command:

    var broker = system.ActorOf(Props.Create(() => new MessageBroker(container.Resolve<ILocateServices>())), "broker");
	broker.Tell(new SendRegistrationReminders());
