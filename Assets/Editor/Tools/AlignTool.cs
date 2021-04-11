using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AlignTool : EditorWindow
{

    [MenuItem("Zen Tools/Align Objects")]
    public static void AlignObjects()
    {
        List<Transform> transforms = new List<Transform>(from g in Selection.gameObjects select g.transform);

        foreach (var t in transforms)
        {
            t.localPosition = RoundPosVector(t.localPosition);
            t.localEulerAngles = RoundAngleVector(t.localEulerAngles);
        }
    }
    public static Vector3Int RoundPosVector(Vector3 v) => new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    public static Vector3 RoundAngleVector(Vector3 vector, float angle = 90)
    {
        Vector3 v = Vector3.zero;
        v.x = Mathf.Round(vector.x / angle) * angle;
        v.y = Mathf.Round(vector.y / angle) * angle;
        v.z = Mathf.Round(vector.z / angle) * angle;
        return v;
    } 
}
