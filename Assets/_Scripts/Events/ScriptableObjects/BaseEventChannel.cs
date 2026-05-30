using UnityEngine;
using System;

public abstract class BaseEventChannelSO<T> : ScriptableObject
{
    public Action<T> OnEventRaised;

    public void RaiseEvent(T item)
    {
        OnEventRaised?.Invoke(item);
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
