using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafting
{
    [Serializable]
    public class PartData : IComparable<PartData>
    {
        public PartID ID;
        public CraftingMaterialID material;
        public int quality;

        public PartData(PartID iD, CraftingMaterialID material, int quality = 0)
        {
            ID = iD;
            this.material = material;
            this.quality = quality;
        }

        public int CompareTo(PartData other) => ID.CompareTo(other.ID);
    }
}
