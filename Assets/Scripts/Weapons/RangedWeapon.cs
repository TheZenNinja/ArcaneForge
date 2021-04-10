using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class RangedWeapon : WeaponBase
    {
        public AmmoCounter ammo = new AmmoCounter(8);

        public bool isReloading;

        public override void UpdateUI()
        {
            if (isReloading)
                StaticRefences.equipmentUI.UpdateAmmoToReloading();
            else
                StaticRefences.equipmentUI.UpdateAmmo(ammo);
        }
        public override void ApplyDataInfo(AnimDataStatePass data)
        {
            switch (data)
            {
                case AnimDataStatePass.isReloadingTrue:
                    isReloading = true;
                    break;
                case AnimDataStatePass.isReloadingFalse:
                    isReloading = false;
                    break;
            }
        }
        public Vector3 GetCamDirFromPoint(Vector3 position)
        {
            FPCameraController cam = StaticRefences.camController;

            RaycastHit hit;
            Vector3 dir;
            if (cam.GetRaycast(out hit, maxDist: 100))
            {
                dir = (hit.point - position).normalized;
                Debug.Log("Hit");
            }
            else
                dir = (cam.getRay.GetPoint(100) - position).normalized;

            return dir;
        }
    }
}
