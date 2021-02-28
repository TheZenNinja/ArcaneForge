using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class RangedWeapon : WeaponBase
    {
        public Vector3 GetCamDirFromPoint(Vector3 position)
        {
            FPCameraController cam = FindObjectOfType<FPCameraController>();

            RaycastHit hit;
            Vector3 dir;
            if (cam.GetRaycast(out hit, 100))
                dir = (hit.point - position).normalized;
            else
                dir = (cam.getRay.GetPoint(100) - position).normalized;

            return dir;
        }
    }
}
