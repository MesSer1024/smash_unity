using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public interface IMessage
{
}

public interface IMessageListener
{
    void HandleMessage(IMessage msg);
}

public static class MessageManager
{
    private static Dictionary<Type, List<IMessageListener>> _callbacks = new Dictionary<Type, List<IMessageListener>>();
    private static List<IMessage> _messages = new List<IMessage>();

    public static void AddListener(Type type, IMessageListener listener)
    {
        if (!_callbacks.ContainsKey(type))
            _callbacks.Add(type, new List<IMessageListener>() { listener });
        else
            _callbacks[type].Add(listener);
    }

    public static void RemoveListener(Type type, IMessageListener listener)
    {
        _callbacks[type].Remove(listener);
    }

    public static void QueueMessage(IMessage msg)
    {
        _messages.Add(msg);
    }

    public static void ProcessMessageQueue()
    {
        var messages = _messages.ToArray();
        _messages.Clear();
        foreach(var msg in messages)
        {
            if(_callbacks.ContainsKey(msg.GetType()))
            {
                var listeners = _callbacks[msg.GetType()].ToArray();
                foreach(var listener in listeners)
                {
                    listener.HandleMessage(msg);
                }
            }
        }
    }

    public static void Reset()
    {
        _callbacks.Clear();
        _messages.Clear();
    }
}
