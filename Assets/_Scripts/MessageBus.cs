using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public MessageBus()
    {
        Debug.Log("Message Bus init");
    }

    public void Subscribe<T>(Action<T> handler)
    {
        if (handler == null) return;

        Type messageType = typeof(T);

        if (!_handlers.TryGetValue(messageType, out var list))
        {
            list = new List<Delegate>();
            _handlers[messageType] = list;
        }

        if (!list.Contains(handler))
        {
            list.Add(handler);
        }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        if (handler == null) return;

        Type messageType = typeof(T);

        if (_handlers.TryGetValue(messageType, out var list))
        {
            list.Remove(handler);
            if (list.Count == 0)
            {
                _handlers.Remove(messageType);
            }
        }
    }

    public void Publish<T>(T message)
    {
        Type messageType = typeof(T);

        if (!_handlers.TryGetValue(messageType, out var list) || list.Count == 0)
            return;

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (i < list.Count && list[i] is Action<T> action)
            {
                action.Invoke(message);
            }
        }
    }

    public void Clear()
    {
        _handlers.Clear();
    }
}
