using UnityEngine;

public abstract class TickableObject : MonoBehaviour
{
    public abstract void Tick();
    public virtual void Start()
    {
        TickManager.tick += Tick;
    }
    public virtual void OnDestroy()
    {
        TickManager.tick -= Tick;
    }
}
