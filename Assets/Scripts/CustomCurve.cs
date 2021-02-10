using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomCurve
{
    public float minVal;
    public float maxVal;
    public AnimationCurve curve;
    public float Evaluate(float percent)
    {
        percent = Mathf.Clamp01(percent);

        return Mathf.Lerp(minVal, maxVal, curve.Evaluate(percent));
    }
}