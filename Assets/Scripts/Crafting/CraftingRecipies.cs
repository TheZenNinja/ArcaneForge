using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Crafting
{
    public static class CraftingRecipies
    {
        public class WeaponRecipe
        {
            public string name;
            public List<PartID> inputs;
            public WeaponType output;
            public WeaponRecipe(string name, IEnumerable<PartID> inputs, WeaponType output)
            {
                this.name = name;
                this.inputs = new List<PartID>(inputs);
                this.output = output;
            }
        }
        public static List<WeaponRecipe> anvilRecipies = new List<WeaponRecipe>()
        {
            new WeaponRecipe("Sword",       new PartID[] { new PartID(BladeSubtype.sword), new PartID(MiscPartSubtype.guard), new PartID(HandleSubtype.oneHand)}, WeaponType.sword),
            new WeaponRecipe("Dagger",      new PartID[] { new PartID(BladeSubtype.shortBlade), new PartID(HandleSubtype.oneHand) }, WeaponType.dagger),
            
            new WeaponRecipe("Katana",      new PartID[] { new PartID(BladeSubtype.singleEdge), new PartID(HandleSubtype.twoHand)}, WeaponType.katana),
            new WeaponRecipe("Greatsword",  new PartID[] { new PartID(BladeSubtype.largeBlade), new PartID(MiscPartSubtype.guard), new PartID(HandleSubtype.twoHand)}, WeaponType.greatsword),
            
            new WeaponRecipe("Spear",       new PartID[] { new PartID(BladeSubtype.shortBlade), new PartID(HandleSubtype.staff)}, WeaponType.spear),
            new WeaponRecipe("Staff",       new PartID[] { new PartID(MiscPartSubtype.ornament), new PartID(HandleSubtype.staff)}, WeaponType.staff),
        };
        public static WeaponRecipe GetWeaponRecipe(IEnumerable<PartObject> parts)
        {
            if (parts.Count() < 2)
                return null;
            var partIDs = new List<PartID>(from p in parts select p.data.ID);
            
            return GetWeaponRecipe(partIDs);
        }

        public static WeaponRecipe GetWeaponRecipe(IEnumerable<PartID> parts)
        {
            if (parts.Count() < 2)
                return null;
            var currentParts = new List<PartID>(parts);
            currentParts.Sort();

            return anvilRecipies.Find(x => Enumerable.SequenceEqual(x.inputs.OrderBy(e => e), parts.OrderBy(e => e)));
        }
    }
}
