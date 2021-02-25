using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace Assets.Prototyping
{
    public class CustomTransitionTool : ScriptableObject
    {
        [MenuItem("Tools/Add Custom Transition")]
        public static void AddTransition()
        {
            if (Selection.objects.Length < 2)
                return;
            var getStates = new List<Object>(Selection.objects).FindAll(x => x.GetType() == typeof(AnimatorState));
            if (getStates.Count < 2)
                return;
            var states = new List<AnimatorState>(from s in getStates select s as AnimatorState);

            CustomAnimTransition cat = new CustomAnimTransition();
            cat.destinationState = states[1];
            states[0].AddTransition(cat);


            Debug.Log($"From {states[0]}, to {states[1]}");
        }
    }
}