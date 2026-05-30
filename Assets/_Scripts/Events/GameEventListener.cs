// _Scripts/Events/GameEventListener.cs
using UnityEngine;
using System;

public class GameEventListener<T> : MonoBehaviour
{
    public Action<T> Response;

    protected virtual void OnEnable()
    {
        EventManager.Register<T>(OnEventRaised);
    }

    protected virtual void OnDisable()
    {
        EventManager.Unregister<T>(OnEventRaised);
    }

    private void OnEventRaised(T param)
    {
        Response?.Invoke(param);
    }
}
