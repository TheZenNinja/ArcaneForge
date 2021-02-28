using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Kunai : RangedWeapon
    {
        public GameObject kunaiProjectile;

        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                handAnim.SetTrigger("Fire");
                Shoot();        
            }
        }
        public void Shoot()
        {
            Vector3 dir = GetCamDirFromPoint(rHand.position);

            var c = FindObjectOfType<FPCameraController>();
            var g = Instantiate(kunaiProjectile, rHand.position, Quaternion.identity);//Quaternion.LookRotation(c.forward));
            g.SetActive(true);
            var p = g.GetComponent<Projectile>();
            p.SetSpeed(c.forward * 25);
        }
    }
}