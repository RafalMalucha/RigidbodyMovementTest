using System;

public class MessageBus
{
    public void Subscribe<T>(Action<T> handler)
    {
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
    }

    public void Publish<T>(T message)
    {
    }
}
