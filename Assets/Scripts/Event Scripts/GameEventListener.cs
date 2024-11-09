using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public CustomGameEvent response; //Can be used to link method calls directly in editor

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
        //Debug.Log("Listener has been registered");
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
        //Debug.Log("Listener has been deregistered");
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
        //Debug.Log("Response sent");
    }

}
