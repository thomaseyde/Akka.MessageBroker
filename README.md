# Akka.MessageBroker

A proof of concept of a self registering message broker for command handlers in applications built with Akka.NET.

Implement a command handler:

    public class ReminderIndexHandler : HandlerActor,
        ForCommand<SendRegistrationReminders>
    {
        public ReminderIndexHandler(ReminderIndex reminders)
        {
            Receive<SendRegistrationReminders>(command => reminders.Send());
        }

        public void Handle(SendRegistrationReminders c) 
        {
            // ...
        };
    }
	
Implement a command handler factory:

    public class ReminderIndexHandlerFactory : HandlerFactory<ReminderIndexHandler>
    {
        public ReminderIndexHandlerFactory(ILocateServices services) : base(services) { }

        protected override Props CreateProps()
        {
            var reminders = Resolve<ReminderIndex>();
            return Props.Create(() => new ReminderIndexHandler(reminders));
        }
    }
	
The handler also support events:

    public class ReminderIndexHandler: HandlerActor,
        // ...
        ForEvent<RegistrationRemindersSent>
    {
        public void Apply(RegistrationRemindersSent e) 
        {
            // ...
        }

        // ...
    }

Register factories:

    var container = new UnityContainer();
    container.RegisterHandlerFactoriesInAssembly<ReminderIndex>();
	
Create the router:

	var system = ActorSystem.Create("system");
	router = system.ActorOf(MessageRouter.Create(services), "router");

Send a command:

	router.Tell(new SendRegistrationReminders());

Or publish an event:

	router.Tell(new RegistrationRemindersSent());
