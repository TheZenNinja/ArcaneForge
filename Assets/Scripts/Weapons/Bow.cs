using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    [ExecuteAlways]
    public class Bow : MonoBehaviour
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
        public Animator anim;

        public Transform rHand;
        public GameObject arrow;
        public ParticleSystem overdrawFX;
        
        void Start()
        {
            drawnPercentIndicator.color = defaultColor;
            drawnPercentIndicator.fillAmount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (arrow && rHand)
            {
                arrow.transform.position = rHand.position;
                arrow.transform.forward = rHand.forward;
            }

            if (!Application.isPlaying)
                return;
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
                drawnPercentIndicator.fillAmount = percentDrawn;
                drawnPercentIndicator.color = currentDrawTime == specialDrawTime ? specialColor : defaultColor;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Fire();

                drawnPercentIndicator.color = defaultColor;
                drawnPercentIndicator.fillAmount = 0;

                currentDrawTime = 0;
                anim.SetTrigger("Fire");
                specialDraw = false;
            }
            anim.SetFloat("Bow Draw Percent", drawAnimCurve.Evaluate(percentDrawn));
        }
        public void Fire()
        {
            Transform camPos = FindObjectOfType<FPCameraController>().cam.transform;

            Projectile p = Instantiate(arrowPref, camPos.TransformPoint(Vector3.forward * 2), Quaternion.identity).GetComponent<Projectile>();

            p.SetSpeed(camPos.forward * speedCurve.Evaluate(percentDrawn), gravDelayCurve.Evaluate(percentDrawn));
                
        }
        public int GetDamage() => Mathf.RoundToInt(damageCurve.Evaluate(percentDrawn));

        public void PlayOverdrawnFX()
        {
            overdrawFX.Play();
        }
    }
}
