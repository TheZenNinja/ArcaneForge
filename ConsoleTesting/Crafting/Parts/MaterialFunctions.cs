using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafting.Parts
{
    public static class MaterialFunctions
    {
        public class MaterialData
        {
            public CraftingMaterialID id;
            public List<MaterialAttribute> attributes;

            public MaterialData(CraftingMaterialID id)
            {
                this.id = id;
                this.attributes = new List<MaterialAttribute>();

            }
            public MaterialData(CraftingMaterialID id, MaterialAttribute attribute)
            {
                this.id = id;
                this.attributes = new List<MaterialAttribute>();
                this.attributes.Add(attribute);
            }
            public MaterialData(CraftingMaterialID id, IEnumerable<MaterialAttribute> attributes)
            {
                this.id = id;
                this.attributes = new List<MaterialAttribute>(attributes);
            }
        }

        public static List<MaterialData> metals = new List<MaterialData>()
        {
            //t1
            new MaterialData(new CraftingMaterialID(MetalMaterial.tin)),
            new MaterialData(new CraftingMaterialID(MetalMaterial.copper)),
            //t2
            new MaterialData(new CraftingMaterialID(MetalMaterial.bronze)),
            new MaterialData(new CraftingMaterialID(MetalMaterial.iron)),
            //t3
            new MaterialData(new CraftingMaterialID(MetalMaterial.silver), MaterialAttribute.monsterDmgBonus),
            new MaterialData(new CraftingMaterialID(MetalMaterial.steel)),
        };
    }
}
