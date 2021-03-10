using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Kunai : RangedWeapon
    {
        public override WeaponType weaponType => WeaponType.kunai;
        public float projectileSpeed = 45;
        
        public GameObject kunaiProjectile;

        public override void HandleInput()
        {
            StaticRefences.equipmentUI.UpdateAmmo(ammo);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                handAnim.SetTrigger("Fire");
                Shoot();        
            }
        }
        public void Shoot()
        {
            Vector3 dir = GetCamDirFromPoint(rHand.position);

            var c = StaticRefences.camController;
            var g = Instantiate(kunaiProjectile, rHand.position, Quaternion.identity);//Quaternion.LookRotation(c.forward));
            g.SetActive(true);
            var p = g.GetComponent<Projectile>();
            p.SetSpeed(dir * projectileSpeed);
        }
    }
}