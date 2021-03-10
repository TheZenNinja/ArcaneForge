using System.Collections;
using UnityEngine;
using Player;

namespace Weapons
{
    [System.Serializable]
    public class Gun : RangedWeapon
    {
        public AudioData shoot;
        public AudioData reload;
        public AudioSource source;
        public ParticleSystem muzzleFlash;

        public int RPM = 220;
        public Timer shotDelayTimer = new Timer(0.1f);
        public bool canFire = true;
        public override WeaponType weaponType => WeaponType.gun;

        private void Start()
        {
            ammo.Reload();
            shotDelayTimer.onEnd += () => canFire = true;
            canFire = true;
        }
        public override void HandleInput()
        {


            if (Input.GetKeyDown(KeyCode.R) && !ammo.isFull)
            {
                handAnim.SetTrigger("Reload");
                ammo.Reload();
                source.Play(reload);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && !ammo.isEmpty && canFire)
            {
                handAnim.SetTrigger("Fire");
                source.Play(shoot);
                muzzleFlash.Play();
                ammo--;
                canFire = false;
                shotDelayTimer.Start();
                Shoot();
            }
        }
        public void Shoot()
        {
            var c = StaticRefences.camController;
            RaycastHit hit;
            if (c.GetRaycast(out hit))
            {
                GameObject hitfx = Resources.Load<GameObject>("FXs/Bullet Impact");
                Instantiate(hitfx, hit.point, Quaternion.identity);
            }
        }
        private void FixedUpdate()
        {
            shotDelayTimer.Tick();
        }

        public void UpdateRPM()
        {
            shotDelayTimer.timerLength = 60f / RPM;
            //shotDelayTimer.timerLength = (float)RPM / 3600f;
        }
        private void OnValidate()
        {
            UpdateRPM();
        }
    }
}