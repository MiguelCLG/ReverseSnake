using System;
using System.Collections.Generic;

public static class EventSubscriber
{
    // Metodo para subscrever a um evento, recebe o nome do evento e uma callback
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

    // Metodo para des-subscrever de um evento
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
