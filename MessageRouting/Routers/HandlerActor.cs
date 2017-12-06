using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace MessageRouting.Routers
{
    public class HandlerActor : ReceiveActor
    {
        protected HandlerActor()
        {
            RegisterReceivers();
        }

        private void RegisterReceivers()
        {
            foreach (var type in ReceiverInterfaces())
            {
                RegisterReceiver(type);
            }
        }

        private void RegisterReceiver(Type markerType)
        {
            var messageType = markerType.GenericTypeArguments[0];
            var handler = markerType.GetMethods()[0];
            Receive(messageType, message => handler.Invoke(this, new[] {message}));
        }

        private IEnumerable<Type> ReceiverInterfaces()
        {
            return GetType().GetInterfaces().Where(TypeIsReceiverInterface);
        }

        private static bool TypeIsReceiverInterface(Type type)
        {
            if (!type.IsGenericType) return false;
            var definition = type.GetGenericTypeDefinition();
            return definition == typeof(ForEvent<>) || definition == typeof(ForCommand<>);
        }
    }
}