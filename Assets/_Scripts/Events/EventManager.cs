// _Scripts/Events/EventManager.cs
using System;
using System.Collections.Generic;

public class EventManager
{
    private static readonly Dictionary<Type, IGameEvent> events = new();

    public static GameEvent<T> GetEvent<T>()
    {
        var type = typeof(T);

        if (!events.ContainsKey(type))
        {
            events[type] = new GameEvent<T>();
        }

        return events[type] as GameEvent<T>;
    }

    public static void Raise<T>(T eventParam)
    {
        GetEvent<T>().Raise(eventParam);
    }

    public static void Register<T>(Action<T> listener)
    {
        GetEvent<T>().RegisterListener(listener);
    }

    public static void Unregister<T>(Action<T> listener)
    {
        GetEvent<T>().UnregisterListener(listener);
    }
}
