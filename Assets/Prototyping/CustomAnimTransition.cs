using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class CustomAnimTransition : AnimatorStateTransition
{
    [SerializeField]
    public AnimationCurve transitionCurve { get; set; }

    public CustomAnimTransition()
    { 
        
    }

    
}
