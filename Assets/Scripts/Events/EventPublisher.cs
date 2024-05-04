using System;
using System.Collections.Generic;

public delegate void EventHandler<T>(object sender, T args);

public class EventPublisher<T>
{
    public event EventHandler<T> MyEvent;
    public void RaiseEvent(T args)
    {
        MyEvent?.Invoke(this, args);
    }
}
