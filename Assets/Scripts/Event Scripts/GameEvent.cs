using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEventSO", order = 1)]
public class GameEvent : ScriptableObject //Acts as the inbetween
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    //Broadcast / Raise event through different method signatures

    public void Raise(Component sender, object data)
    {
        //Debug.Log("Raise has been called");
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(sender, data);
            //Debug.Log("Event has successfully been raised");
        }
    }

    //Manage Listeners

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
