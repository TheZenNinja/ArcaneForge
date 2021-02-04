using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Crafting
{
    public class ListZone : Zone
    {
        Collider c;
        public bool checkInFixedUpdate;
        public List<GameObject> objectsInZone;
        private void Start()
        {
            c = GetComponent<Collider>();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (!objectsInZone.Contains(other.gameObject))
                objectsInZone.Add(other.gameObject);
        }
        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            if (objectsInZone.Contains(other.gameObject))
                objectsInZone.Remove(other.gameObject);
        }

        protected void FixedUpdate()
        {
            if (checkInFixedUpdate && objectsInZone.Count > 0)
                CheckZone();
        }
        protected void CheckZone()
        {
            foreach (var o in objectsInZone)
                if (!c.CollidersAreWithinBounds(o.GetComponents<Collider>()))
                {
                    onExit?.Invoke(o);
                    objectsInZone.Remove(o);
                    Debug.Log($"{o.name}, Not In Zone");
                    CheckZone();
                    return;
                }
        }
    }
}