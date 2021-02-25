using System;
using UnityEngine;

public abstract class DraggableObject : MonoBehaviour, IInteractable
{
    private bool _colEnabled;
    protected bool CollidersEnabled
    {
        get
        {
            return _colEnabled;
        }
        set 
        {
            _colEnabled = value;
            if (colliders != null && colliders.Length > 0)
            foreach (var c in colliders)
                c.enabled = value;
        }
    }


    protected Collider[] colliders;
    protected Rigidbody rb;
    public bool beingDragged;
    public Vector3 centerOffset;
    public Vector3 position
    {
        get => transform.TransformPoint(centerOffset);
        set => transform.position = value - transform.TransformVector(centerOffset);
    }
    public Quaternion rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }
    public Vector3 localAngles
    {
        get => transform.localEulerAngles;
        set => transform.localEulerAngles = value;
    }
    public virtual bool Interact(PlayerData p) => false;

    protected void Awake()
    {
        colliders = GetComponents<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    public virtual void StartDrag(Transform trans)
    {
        transform.SetParent(trans);
        beingDragged = true;
        TogglePhys(false);
        //CollidersEnabled = false;
    }
    public virtual void StopDrag()
    {
        transform.SetParent(null);
        beingDragged = false;
        TogglePhys(true);
        //CollidersEnabled = true;
    }
    public void TogglePhys(bool active)
    {
        rb.isKinematic = !active;
        rb.freezeRotation = !active;
        if (!active)
            rb.velocity = Vector3.zero;
        foreach (var c in colliders)
            c.isTrigger = !active;
    }
}