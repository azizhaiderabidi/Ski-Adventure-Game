using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Event Channel")]
public class EventChannelSO : ScriptableObject
{

    public UnityAction OnEventRaised;

    public void Raise()
    {
        OnEventRaised?.Invoke();
    }
   
}
