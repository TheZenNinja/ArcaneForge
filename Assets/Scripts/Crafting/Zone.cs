using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Zone : MonoBehaviour
{
    public Action<GameObject> onEnter;
    public Action<GameObject> onExit;
    protected virtual void OnTriggerEnter(Collider other) => onEnter?.Invoke(other.gameObject);
    protected virtual void OnTriggerExit(Collider other) => onExit?.Invoke(other.gameObject);
}
