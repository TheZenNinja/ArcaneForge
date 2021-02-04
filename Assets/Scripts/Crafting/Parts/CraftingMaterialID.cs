using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafting
{
    [Serializable]
    public struct CraftingMaterialID : IComparable<CraftingMaterialID>
    {
        public CraftingMaterialType type;
        public int subtype;

        public CraftingMaterialID(CraftingMaterialType materialType, int material = 0)
        {
            this.type = materialType;
            this.subtype = material;
        }
        public CraftingMaterialID(MetalMaterial material)
        {
            this.type = CraftingMaterialType.metal;
            this.subtype = (int)material;
        }
        public int CompareTo(CraftingMaterialID other)
        {
            int typeTest = type.CompareTo(other.type);
            if (typeTest != 0)
                return typeTest;
            return subtype.CompareTo(other.subtype);
        }
    }
}
