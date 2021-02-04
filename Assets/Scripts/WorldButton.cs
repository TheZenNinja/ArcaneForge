using System;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour, IInteractable
{
    //for some reason this is required to have it show up in the inspector
    [Serializable]
    public class EventHook : UnityEvent<Player> { }
    public EventHook onInteract;

    public bool Interact(Player p)
    {
        onInteract.Invoke(p);
        return true;
    }
}
