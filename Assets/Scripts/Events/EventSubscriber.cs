using System;
using System.Collections.Generic;

public static class EventSubscriber
{
    // Method to subscribe to an event by name
    public static void SubscribeToEvent(string eventName, EventHandler<object> eventHandler)
    {
        EventPublisher<object> publisher = EventRegistry.GetEventPublisher(eventName);
        if (publisher != null)
        {
            publisher.MyEvent += eventHandler;
        }
        else
        {
            Console.WriteLine($"Event '{eventName}' does not exist.");
        }
    }

    // Method to unsubscribe from an event by name
    public static void UnsubscribeFromEvent(string eventName, EventHandler<object> eventHandler)
    {
        EventPublisher<object> publisher = EventRegistry.GetEventPublisher(eventName);
        if (publisher != null)
        {
            publisher.MyEvent -= eventHandler;
        }
        else
        {
            Console.WriteLine($"Event '{eventName}' does not exist.");
        }
    }
}
