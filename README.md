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
        HandlerFactory,
        ForCommand<SendRegistrationReminders>
    {
        public ReminderIndexHandlerFactory(ILocateServices services) : base(services) { }

        protected override Props CreateProps()
        {
            var reminders = Resolve<ReminderIndex>();
            return Props.Create(() => new ReminderIndexHandler(reminders));
        }
    }
	
The handler factory also support events:

    public class ReminderIndexHandlerFactory :
        HandlerFactory,
        ForCommand<SendRegistrationReminders>,
        ForEvent<RegistrationRemindersSent>
    {
		// ...
    }

Register factories:

    var container = new UnityContainer();
    container.RegisterHandlerFactoriesInAssembly<ReminderIndex>();
	
Send a command:

	var system = ActorSystem.Create("system");
	commandRouter = system.ActorOf(CommandRouter.Create(services), "command-router");
	commandRouter.Tell(new SendRegistrationReminders());

Publish an event:

	var system = ActorSystem.Create("system");
	eventRouter = system.ActorOf(EventRouter.Create(services), "event-router");
	eventRouter.Tell(new RegistrationRemindersSent());
