using System;
using System.Collections.Generic;
using System.Linq;

public static class EventRegistry
{
    private static Dictionary<string, EventPublisher<object>> eventDictionary = new Dictionary<string, EventPublisher<object>>();

    // Method to register an event with a given name
    public static void RegisterEvent(string eventName)
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new EventPublisher<object>();
        }
    }

    public static Dictionary<string, EventPublisher<object>> GetAllEvents()
    {
        return eventDictionary;
    }

    // Method to get the EventPublisher instance for a given event name
    public static EventPublisher<object> GetEventPublisher(string eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            return eventDictionary[eventName];
        }
        return null;
    }
}
