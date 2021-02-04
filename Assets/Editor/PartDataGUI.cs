using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Crafting;

[CustomPropertyDrawer(typeof(PartData))]
[CanEditMultipleObjects]
public class PartDataGUI : PropertyDrawer
{
    //https://docs.unity3d.com/Manual/editor-PropertyDrawers.html
    //https://catlikecoding.com/unity/tutorials/editor/custom-data/
    private const int numProperties = 3;
    private const float lineHeight = 18;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int oldIndentLevel = EditorGUI.indentLevel;
        position.height = numProperties * lineHeight;
        EditorGUI.BeginProperty(position, label, property);
        position.height = lineHeight;
    
        EditorGUI.PropertyField(position, property.FindPropertyRelative("ID"), new GUIContent("Part ID"));
        position.y += lineHeight;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("material"), new GUIContent("Material ID"));
        position.y += lineHeight;
        EditorGUI.PropertyField(position, property.FindPropertyRelative("quality"), new GUIContent("Part Quality"));
    
        EditorGUI.EndProperty();
    
        EditorGUI.indentLevel = oldIndentLevel;
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return lineHeight * numProperties;
    }
}
