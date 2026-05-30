using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    public static TimerSystem instance;
    private void Awake()
    {
        instance = this;
    }


    List<Timer> timers = new List<Timer>();

    private void Update()
    {
        for (int i = 0; i < timers.Count; i++)
            if (timers[i] != null)
                timers[i].Tick();
    }

    public void AddTimer(Timer timer)
    {
        timers.Add(timer);
    }
    public void RemoveTimer(Timer timer)
    {
        timers.Remove(timer);
    }
}

public class Timer
{
    public float Time;
    public float StopAt;

    public Action OnCompleted;
    public Action OnTick;

    public bool IsRunning { get; private set; }

    public Timer(float time , Action OnCompleted , bool autoKill = false)
    {
        Time = 0;
        StopAt = time;

        IsRunning = true;

        TimerSystem.instance.AddTimer(this);

        this.OnCompleted = OnCompleted;

        this.OnCompleted += () => {
            IsRunning = false;

            if (autoKill) Kill();
        };
    }

    public void Tick()
    {
        if (!IsRunning) return;

        Time += UnityEngine.Time.deltaTime;

        if (Time > StopAt)
        {
            OnCompleted?.Invoke();
        }

        OnTick?.Invoke();
    }

    public void Kill()
    {
        TimerSystem.instance.RemoveTimer(this);
    }
}
