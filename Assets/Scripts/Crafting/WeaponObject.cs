using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Weapons;

namespace Crafting
{
    public class WeaponObject : DraggableObject
    {
        public WeaponType type;
        public List<WeaponMaterialRenderers> renderers;
        [HideInInspector]
        public List<PartData> parts;

        protected bool initialized;

        private void Start()
        {
            if (!initialized)
                Setup();
        }
        public void Setup()
        {
            initialized = true;

            parts.Sort();

            renderers.Sort();

            for (int i = 0; i < parts.Count; i++)
            {
                var data = MaterialDataFunctions.FindMaterial(parts[i].material);
                Debug.Log(data.GetWeaponShader);
                renderers[i].SetMaterial(data.GetWeaponShader);
            }

        }

        public static WeaponObject Create(Vector3 position, WeaponType type, IEnumerable<PartData> parts)
        {
            string path = "Objects/Weapons/" + type.ToString().Capitalize();

            GameObject g = Resources.Load<GameObject>(path);

            WeaponObject w = Instantiate(g, position, Quaternion.identity).GetComponent<WeaponObject>();
            w.parts = new List<PartData>(parts);
            w.Setup();
            return w;
        }
    }
}