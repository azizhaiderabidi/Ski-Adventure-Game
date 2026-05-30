// _Scripts/Events/GameStartEventChannelSO.cs
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameStartEventChannel")]
public class GameStartEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
}
