// _Scripts/Events/GameEvent.cs
using System;

public class GameEvent<T> : IGameEvent
{
    public event Action<T> OnEventRaised;

    public void Raise(T param)
    {
        OnEventRaised?.Invoke(param);
    }

    public void RegisterListener(Action<T> listener)
    {
        OnEventRaised += listener;
    }

    public void UnregisterListener(Action<T> listener)
    {
        OnEventRaised -= listener;
    }
}
