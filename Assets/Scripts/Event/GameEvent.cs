using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    public UnityAction OnRaised;

    public void Raise()
    {
        OnRaised?.Invoke();
    }
}

