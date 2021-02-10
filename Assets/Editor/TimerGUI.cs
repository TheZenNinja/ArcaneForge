using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Crafting;

[CustomPropertyDrawer(typeof(Timer))]
[CanEditMultipleObjects]
public class TimerGUI : PropertyDrawer
{
    //https://docs.unity3d.com/Manual/editor-PropertyDrawers.html
    //https://catlikecoding.com/unity/tutorials/editor/custom-data/

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int oldIndentLevel = EditorGUI.indentLevel;

        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        contentPosition.width /= 2;

        var length = property.FindPropertyRelative("timerLength");
        var currentTime = property.FindPropertyRelative("currentTime");

        EditorGUI.indentLevel = 0;
        EditorGUIUtility.labelWidth = 100;
        EditorGUI.PropertyField(contentPosition, length, new GUIContent("Timer Length:"));
        contentPosition.x += contentPosition.width;
        contentPosition.x += 7;
        EditorGUI.PropertyField(contentPosition, currentTime, new GUIContent("Current Time:"));

        EditorGUI.EndProperty();

        EditorGUI.indentLevel = oldIndentLevel;
    }
}
