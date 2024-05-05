using System;
using System.Collections.Generic;
using System.Linq;

public static class EventRegistry
{
    private static Dictionary<string, EventPublisher<object>> eventDictionary = new Dictionary<string, EventPublisher<object>>();

    // Cria um evento com um nome
    public static void RegisterEvent(string eventName)
    {
        if (!eventDictionary.ContainsKey(eventName)) //  Se nao existe o evento, entao cria
        {
            eventDictionary[eventName] = new EventPublisher<object>();
        }
    }

    // funcao para ir buscar todos os eventos criados
    public static Dictionary<string, EventPublisher<object>> GetAllEvents()
    {
        return eventDictionary;
    }

    // metodo para ir buscar o publisher correspondente ao evento com o nome eventName
    public static EventPublisher<object> GetEventPublisher(string eventName)
    {
        if (eventDictionary.ContainsKey(eventName)) 
        {
            return eventDictionary[eventName];
        }
        return null;
    }
}
