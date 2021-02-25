using System;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour, IInteractable
{
    //for some reason this is required to have it show up in the inspector
    [Serializable]
    public class EventHook : UnityEvent<PlayerData> { }
    public EventHook onInteract;

    public bool Interact(PlayerData p)
    {
        onInteract.Invoke(p);
        return true;
    }
}
