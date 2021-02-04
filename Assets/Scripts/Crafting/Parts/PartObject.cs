using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Crafting
{
    public class PartObject : HeatableObject
    {
        public bool allowAttach;
        public PartData data;
        //public PartType partType;
        //public int subtype = 0;
        public bool allowCraft => currentHeatLevel == 0;

        public override void Setup()
        {
            material = (MetalMaterial)data.material.subtype;
            base.Setup();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.TransformPoint(centerOffset), .1f);
        }

        
        public static PartObject Create(Vector3 pos, PartData data)
        {
            string path = $"Objects/Parts/{data.ID}";
            Debug.Log(path);
            var r = Resources.Load<GameObject>(path);

            PartObject po = Instantiate(r, pos, Quaternion.identity).GetComponent<PartObject>();
            po.data = data;
            po.Setup();
            po.MaxHeat();

            return po;
        }
    }
    //[CustomEditor(typeof(PartObject))]
    //public class PartObjectGUI : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        var p = (PartObject)target;
    //        p.partType = (PartType)EditorGUILayout.EnumPopup("Part Type:", p.partType);
    //
    //        switch (p.partType)
    //        {
    //            case PartType.handle:
    //                p.subtype = (int)(HandleSubtype)EditorGUILayout.EnumPopup("Handle Type:", (HandleSubtype)p.subtype);
    //                break;
    //            case PartType.blade:
    //                p.subtype = (int)(BladeSubtype)EditorGUILayout.EnumPopup("Blade Type:", (BladeSubtype)p.subtype);
    //                break;
    //            case PartType.guard:
    //                EditorGUILayout.TextArea("Currently only one guard");
    //                break;
    //            default:
    //                EditorGUILayout.TextArea("Invalid Part Type");
    //                break;
    //        }
    //
    //    }
    //}

}