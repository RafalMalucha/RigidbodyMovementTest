using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus
{
    private Dictionary<Type, List<Delegate>> _handlers = new();

    public MessageBus()
    {
        Debug.Log("Message Bus init");
    }

    public void Subscribe<T>(Action<T> handler)
    {
        Type messageType = typeof(T);

        if (!_handlers.ContainsKey(messageType))
        {
            _handlers.Add(messageType, new List<Delegate>());
        }

        _handlers[messageType].Add(handler);

        Debug.Log(handler + " sub");
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type messageType = typeof(T);

        if (!_handlers.ContainsKey(messageType))
            return;

        _handlers[messageType].Remove(handler);

        Debug.Log(handler + " unsub");
    }

    public void Publish<T>(T message)
    {
        Type messageType = typeof(T);

        if (!_handlers.ContainsKey(messageType))
            return;

        foreach (Delegate handler in _handlers[messageType])
        {
            ((Action<T>)handler)(message);
        }
    }
}
