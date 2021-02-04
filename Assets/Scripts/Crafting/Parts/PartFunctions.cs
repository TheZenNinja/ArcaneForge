using System;
using System.Collections.Generic;
using UnityEngine;
namespace Crafting
{
    public static class PartFunctions
    {
        public class PartData
        {
            public readonly PartID ID;
            public readonly int ingotCost;
            public PartData(PartID ID, int ingotCost)
            {
                this.ID = ID;
                this.ingotCost = ingotCost;
            }
            public override string ToString()
            {
                return base.ToString();
            }
        }

        public static List<PartData> partDatas = new List<PartData>()
        { 
            //handles
            new PartData(new PartID(HandleSubtype.oneHand), 1),
            new PartData(new PartID(HandleSubtype.twoHand), 2),
            new PartData(new PartID(HandleSubtype.staff), 4),
            //guards
            new PartData(new PartID(MiscPartSubtype.guard), 3),
            new PartData(new PartID(MiscPartSubtype.ornament), 1),
            //blades
            new PartData(new PartID(BladeSubtype.sword), 4),
            new PartData(new PartID(BladeSubtype.singleEdge), 4),
            new PartData(new PartID(BladeSubtype.largeBlade), 6),
            new PartData(new PartID(BladeSubtype.shortBlade), 2),
        };


        public static int ClampSubtypeValue<T>(int subtype)
        {
            int max = Enum.GetNames(typeof(T)).Length;
            return UnityEngine.Mathf.Clamp(subtype, 0, max);
        }
    }
}
