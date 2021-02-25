using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    [ExecuteAlways]
    public class Bow : WeaponBase
    {
        public Image drawnPercentIndicator;
        public Color defaultColor;
        public Color specialColor;

        public GameObject arrowPref;

        public float currentDrawTime;
        public float maxDamageDrawTime = 1f;
        public float specialDrawTime = 2f;
        public float percentDrawn => Mathf.Clamp01(currentDrawTime/ maxDamageDrawTime);
        public bool specialDraw;

        public CustomCurve speedCurve;
        public CustomCurve damageCurve;
        public CustomCurve gravDelayCurve;
        public AnimationCurve drawAnimCurve;
        public Vector3 arrowSpawnOffset;
        public float arrowRotOffset;
        public Transform rHand;
        public GameObject arrow;
        public ParticleSystem overdrawFX;
        
        void Update()
        {
            if (arrow && rHand)
            {
                arrow.transform.position = rHand.position;
                arrow.transform.rotation = rHand.rotation;
            }
        }
        public override void HandleInput()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (currentDrawTime < specialDrawTime)
                    currentDrawTime += Time.deltaTime;
                else if (!specialDraw)
                {
                    specialDraw = true;
                    currentDrawTime = specialDrawTime;
                    PlayOverdrawnFX();
                }

                if (drawnPercentIndicator)
                {
                    drawnPercentIndicator.fillAmount = percentDrawn;
                    drawnPercentIndicator.color = currentDrawTime == specialDrawTime ? specialColor : defaultColor;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (specialDraw)
                    OverdrawnFire();
                else
                    Fire();

                if (drawnPercentIndicator)
                {
                    drawnPercentIndicator.color = defaultColor;
                    drawnPercentIndicator.fillAmount = 0;
                }

                currentDrawTime = 0;
                handAnim.SetTrigger("Fire");
                specialDraw = false;
            }
            handAnim.SetFloat("Bow Draw Percent", drawAnimCurve.Evaluate(percentDrawn));
        }
        public void Fire()
        {
            FPCameraController cam = FindObjectOfType<FPCameraController>();

            RaycastHit hit;
            Vector3 dir;
            if (cam.GetRaycast(out hit, 100))
                dir = (hit.point-rHand.TransformPoint(arrowSpawnOffset)).normalized;
            else 
                dir = (cam.getRay.GetPoint(100)-rHand.TransformPoint(arrowSpawnOffset)).normalized;

            Projectile p = Instantiate(arrowPref, rHand.TransformPoint(arrowSpawnOffset), Quaternion.identity).GetComponent<Projectile>();
            p.SetSpeed(dir * speedCurve.Evaluate(percentDrawn), gravDelayCurve.Evaluate(percentDrawn));

            overdrawFX.Stop();
            overdrawFX.Clear();
        }
        public void OverdrawnFire()
        {
            FPCameraController cam = FindObjectOfType<FPCameraController>();

            RaycastHit hit;
            Vector3 dir;
            if (cam.GetRaycast(out hit, 100))
                dir = (hit.point - rHand.TransformPoint(arrowSpawnOffset)).normalized;
            else
                dir = (cam.getRay.GetPoint(100) - rHand.TransformPoint(arrowSpawnOffset)).normalized;
            
            //change this so it splits in the arrow class instead 
            Projectile p1 = Instantiate(arrowPref, rHand.TransformPoint(arrowSpawnOffset), Quaternion.identity).GetComponent<Projectile>();
            p1.SetSpeed(dir * speedCurve.Evaluate(percentDrawn), gravDelayCurve.Evaluate(percentDrawn)); 

            Projectile p2 = Instantiate(arrowPref, rHand.TransformPoint(arrowSpawnOffset), Quaternion.identity).GetComponent<Projectile>();
            p2.SetSpeed((dir + Vector3.up * .1f).normalized * speedCurve.Evaluate(percentDrawn), gravDelayCurve.Evaluate(percentDrawn)); 

            Projectile p3 = Instantiate(arrowPref, rHand.TransformPoint(arrowSpawnOffset), Quaternion.identity).GetComponent<Projectile>();
            p3.SetSpeed((dir - Vector3.up * .1f).normalized * speedCurve.Evaluate(percentDrawn), gravDelayCurve.Evaluate(percentDrawn));

            overdrawFX.Stop();
            overdrawFX.Clear();
        }

        public override void Equip(EquipmentController equipment)
        {
            base.Equip(equipment);
            rHand = equipment.weaponR;
            drawnPercentIndicator = equipment.UI.cursorCircle;
        }
        public int GetDamage() => Mathf.RoundToInt(damageCurve.Evaluate(percentDrawn));

        public void PlayOverdrawnFX()
        {
            overdrawFX.Play();
        }
        private void OnDrawGizmosSelected()
        {
            if (rHand)
                Gizmos.DrawSphere(rHand.TransformPoint(arrowSpawnOffset), .1f);
        }
    }
}
