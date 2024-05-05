using System;
using System.Collections.Generic;

// Evento criado para ser publicado
public delegate void EventHandler<T>(object sender, T args);

//Class para guardar os dados do evento e invocar
public class EventPublisher<T>
{
    public event EventHandler<T> MyEvent;

    //Invoca o evento
    public void RaiseEvent(T args)
    {
        MyEvent?.Invoke(this, args);
    }
}
