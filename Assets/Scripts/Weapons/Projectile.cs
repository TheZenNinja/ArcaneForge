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
        [Space]
        [Header("Collision Detection")]
        public LayerMask targetMask;
        public float raycastRadius = .1f;
        public Vector3 embedPosOffset;
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
                //Ray r = new Ray(transform.position, velDir);
                //if (Physics.SphereCast(r,raycastRadius, out hit, rb.velocity.magnitude * Time.fixedDeltaTime, targetMask))
                //{
                //    transform.position = hit.point;
                //    transform.parent = hit.collider.transform;
                //    ToggleActive(false);
                //}

                RaycastHit hit;
                if (rb.SweepTest(velDir, out hit, rb.velocity.magnitude * Time.fixedDeltaTime))
                {
                    transform.position = hit.point - transform.TransformVector(embedPosOffset);
                    Debug.DrawRay(hit.point, hit.normal);
                    //transform.forward = -hit.normal;
                    //transform.parent = hit.collider.transform;
                    ToggleActive(false);
                    return;
                }    

                timeAlive += Time.fixedDeltaTime;
                if (timeBeforeGrav < timeAlive)
                    rb.velocity += -Vector3.up * gravity * Time.fixedDeltaTime;

                transform.forward = velDir;
                if (timeAlive > 60 * 5)
                    Destroy(gameObject);
            }
        }
        public void SetSpeed(Vector3 vel, bool resetTimeAlive = true)
        {
            ToggleActive(true);
            if (resetTimeAlive)
                timeAlive = 0;
            rb.velocity = vel;
            transform.forward = velDir;

        }
        public void SetSpeed(Vector3 vel, float timeBeforeGrav, bool resetTimeAlive = true)
        {
            this.timeBeforeGrav = timeBeforeGrav;
            SetSpeed(vel, resetTimeAlive);
        }

        public void ToggleActive(bool active = true)
        {
            this.active = active;
            if (!active)
                rb.velocity = Vector3.zero;

            rb.collisionDetectionMode = active ? CollisionDetectionMode.Continuous : CollisionDetectionMode.Discrete;
            rb.isKinematic = !active;
            col.enabled = active;
            Destroy(gameObject, 3);
        }


        public void OnTriggerEnter(Collider other)
        {
            if (targetMask != (targetMask | (1 << other.gameObject.layer)))
            {
                Physics.IgnoreCollision(col, other, true);
                //transform.parent = other.transform;
                //ToggleActive(false);
                //var h = other.GetComponent<Health>();
                //if ()
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.TransformVector(embedPosOffset), .1f);
        }
    }
}
