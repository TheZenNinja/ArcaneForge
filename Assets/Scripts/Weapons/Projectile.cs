using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody rb;
        Collider col;

        public bool active;
        public Vector3 velDir => rb.velocity.normalized;
        public float gravity;
        public float timeBeforeGrav;
        private float timeAlive;
        public LayerMask targetMask;
        private Vector3 hitPosOffset;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
        }
        public void FixedUpdate()
        {
            if (active)
            {
                //RaycastHit hit;
                //if (rb.SweepTest(velDir, out hit, rb.velocity.magnitude))
                //{
                //    transform.position = hit.point;
                //    transform.parent = hit.collider.transform;
                //    ToggleActive(false);
                //}    

                timeAlive += Time.fixedDeltaTime;
                if (timeBeforeGrav < timeAlive)
                    rb.velocity += -Vector3.up * gravity * Time.fixedDeltaTime;

                transform.forward = velDir;
            }
        }
        public void SetSpeed(Vector3 vel, float timeBeforeGrav = 1, bool resetTimeAlive = true)
        {
            ToggleActive(true);
            if (resetTimeAlive)
                timeAlive = 0;
            rb.velocity = vel;
            this.timeBeforeGrav = timeBeforeGrav;
                transform.forward = velDir;
        }

        public void ToggleActive(bool active = true)
        {
            this.active = active;
            if (!active)
                rb.velocity = Vector3.zero;

            rb.collisionDetectionMode = active ? CollisionDetectionMode.Continuous : CollisionDetectionMode.Discrete;
            rb.isKinematic = !active;
            col.enabled = active;
        }


        public void OnTriggerEnter(Collider other)
        {
            if (targetMask == (targetMask | (1 << other.gameObject.layer)))
            {
                transform.parent = other.transform;
                ToggleActive(false);
                //var h = other.GetComponent<Health>();
                //if ()
            }
        }
    }
}
