using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Crafting;

[CustomPropertyDrawer(typeof(CraftingMaterialID))]
[CanEditMultipleObjects]
public class CraftingMaterialIDGUI : PropertyDrawer
{
    //https://docs.unity3d.com/Manual/editor-PropertyDrawers.html
    //https://catlikecoding.com/unity/tutorials/editor/custom-data/

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int oldIndentLevel = EditorGUI.indentLevel;

        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        contentPosition.width /= 2;

        var type = property.FindPropertyRelative("type");
        var subtype = property.FindPropertyRelative("subtype");

        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, type, GUIContent.none);
        contentPosition.x += contentPosition.width;
        contentPosition.x += 7;
        EditorGUIUtility.labelWidth = 14f;
        
        switch ((CraftingMaterialType)type.enumValueIndex)
        {
            case CraftingMaterialType.metal:
                subtype.intValue = (int)(MetalMaterial)EditorGUI.EnumPopup(contentPosition, (MetalMaterial)subtype.intValue);
                break;
        }

        EditorGUI.EndProperty();

        EditorGUI.indentLevel = oldIndentLevel;
    }
}
