using System.Collections;
using UnityEngine;

namespace Crafting
{
    public class IngotGameObject : HeatableObject
    {
        private void OnValidate()
        {
            UpdateMetalData();
        }

        public static IngotGameObject Spawn(CraftingMaterialID material, Vector3 position) => Spawn((MetalMaterial)material.subtype, position);
        public static IngotGameObject Spawn(MetalMaterial material, Vector3 position)
        {
            GameObject pref = Resources.Load<GameObject>("Objects/Ingot");
            IngotGameObject i = Instantiate(pref, position, Quaternion.identity).GetComponent<IngotGameObject>();

            i.material = material;
            i.Setup();
            return i;
        }
    }
    //set up part object and ingot object spawning
}