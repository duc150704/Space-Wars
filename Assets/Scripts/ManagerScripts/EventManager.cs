using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEventType
{
    StartPlaying,

    EnemyDead,
    BossAppear,
    BossHealthChange,

    ShieldOn,
    PlayerDead,
    PlayerRespawn,
}
public static class EventManager
{
    static Dictionary<EEventType, List<Action>> _listeners = new Dictionary<EEventType, List<Action>>();
    //static Dictionary<EEventType, List<Action<IEventData>>> _listenersWithParams
    //    = new Dictionary<EEventType, List<Action<IEventData>>>();

    //public static void Subscribe(EEventType eventType, Action<IEventData> action)
    //{
    //    if (!_listenersWithParams.ContainsKey(eventType))
    //    {
    //        _listenersWithParams.Add(eventType, new List<Action<IEventData>>());
    //    }
    //    _listenersWithParams[eventType].Add(action);
    //}

    public static void Subscribe(EEventType eventType, Action action)
    {
        if (!_listeners.ContainsKey(eventType))
        {
            _listeners.Add(eventType, new List<Action>());
        }
        _listeners[eventType].Add(action);
    }

    //public static void Unsubscribe(EEventType eventType, Action<IEventData> action)
    //{
    //    if (!_listenersWithParams.ContainsKey(eventType))
    //    {
    //        return;
    //    }
    //    _listenersWithParams[eventType].Remove(action);
    //}
    public static void Unsubscribe(EEventType eventType, Action action)
    {
        if (!_listeners.ContainsKey(eventType))
        {
            return;
        }
        _listeners[eventType].Remove(action);
    }

    //public static void Notify(EEventType eventType, IEventData eventData)
    //{
    //    if (!_listenersWithParams.ContainsKey(eventType))
    //        return;
    //    foreach (var item in _listenersWithParams[eventType])
    //    {
    //        item?.Invoke(eventData);
    //    }
    //}
    public static void Notify(EEventType eventType)
    {
        if (!_listeners.ContainsKey(eventType))
            return;
        foreach (var item in _listeners[eventType])
        {
            item?.Invoke();
        }
    }
}

