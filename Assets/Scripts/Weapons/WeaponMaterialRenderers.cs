using System;
using UnityEngine;
using Crafting;

namespace Weapons
{
    [Serializable]
    public class WeaponMaterialRenderers : IComparable<WeaponMaterialRenderers>
    {
        public string name => type.ToString();
        public PartType type;
        public Renderer[] renderers;

        public void SetMaterial(Material mat)
        {
            foreach (Renderer r in renderers)
                r.material = new Material(mat);
        }
        public int CompareTo(WeaponMaterialRenderers other)
        {
            return type.CompareTo(other.type);
        }
    }
}
