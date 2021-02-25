using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Kunai : WeaponBase
    {
        public GameObject kunaiProjectile;

        public override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();        
            }
        }
        public void Shoot()
        {
            var c = FindObjectOfType<FPCameraController>();
            var g = Instantiate(kunaiProjectile, c.getRay.GetPoint(1), Quaternion.LookRotation(c.forward));
            g.SetActive(true);
            var p = g.GetComponent<Projectile>();
            p.SetSpeed(c.forward * 10);
        }
    }
}