using UnityEngine;
using UnityEditor;

public static class EditorList
{
    //https://catlikecoding.com/unity/tutorials/editor/custom-list/

    public static void Show(SerializedProperty list, bool showListSize = true)
    {
        EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel++;
        if (list.isExpanded)
        {
            if (showListSize)
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));

            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
        }
        EditorGUI.indentLevel--;
    }
}