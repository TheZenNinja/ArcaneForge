using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Crafting;

[CustomPropertyDrawer(typeof(AudioData))]
[CanEditMultipleObjects]
public class AudioDataGUI : PropertyDrawer
{
    //https://docs.unity3d.com/Manual/editor-PropertyDrawers.html
    //https://catlikecoding.com/unity/tutorials/editor/custom-data/

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int oldIndentLevel = EditorGUI.indentLevel;

        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        contentPosition.width /= 2;

        var clip = property.FindPropertyRelative("clip");
        var volume = property.FindPropertyRelative("volume");

        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, clip, GUIContent.none);
        contentPosition.x += contentPosition.width;
        contentPosition.x += 7;
        EditorGUIUtility.labelWidth = 50f;

        EditorGUI.Slider(contentPosition, volume, 0, 1);

        EditorGUI.EndProperty();

        EditorGUI.indentLevel = oldIndentLevel;
    }
}
