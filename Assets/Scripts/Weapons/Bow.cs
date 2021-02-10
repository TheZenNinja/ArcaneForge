using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
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
        public bool specialDraw => currentDrawTime >= specialDrawTime;

        public CustomCurve speedCurve;
        public CustomCurve damageCurve;
        public CustomCurve gravDelayCurve;
        public AnimationCurve drawAnimCurve;
        public Animator anim;

        void Start()
        {
            drawnPercentIndicator.color = defaultColor;
            drawnPercentIndicator.fillAmount = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                currentDrawTime += Time.deltaTime;
                currentDrawTime = Mathf.Clamp(currentDrawTime, 0, specialDrawTime);

                drawnPercentIndicator.fillAmount = percentDrawn;
                drawnPercentIndicator.color = currentDrawTime == specialDrawTime ? specialColor : defaultColor;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Debug.Log("Speed: " + speedCurve.Evaluate(percentDrawn));
                Debug.Log("Damage: " + GetDamage());
                Debug.Log("GravDelay: " + gravDelayCurve.Evaluate(percentDrawn));

                drawnPercentIndicator.color = defaultColor;
                drawnPercentIndicator.fillAmount = 0;

                currentDrawTime = 0;
                anim.SetTrigger("Fire");
            }
            anim.SetFloat("Bow Draw Percent", drawAnimCurve.Evaluate(percentDrawn));
            anim.SetBool("Bow Overdrawn", specialDraw);
        }
        public int GetDamage() => Mathf.RoundToInt(damageCurve.Evaluate(percentDrawn));
    }
}
