using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.OnRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        Event.OnRaised -= OnEventRaised;
    }

    private void OnEventRaised()
    {
        Response?.Invoke();
    }
}

