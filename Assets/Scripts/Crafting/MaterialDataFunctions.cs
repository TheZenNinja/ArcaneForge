using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crafting
{
    public static class MaterialDataFunctions
    {
        public abstract class MaterialData
        {
            public CraftingMaterialID ID;
            public string shaderPath;
            public int moneyValue;
            public abstract string GetName();
            // ore/ingot/part shader
            public virtual Material GetIngredientShader => Resources.Load<Material>($"{shaderPath} Material");
            // weapon material
            public virtual Material GetWeaponShader => Resources.Load<Material>($"{shaderPath} Weapon");
        }
        public class MetalData : MaterialData
        {
            public int maxHeatLevel;
            public int materialLevel;
            public MetalData(MetalMaterial material, int maxHeatLevel, int materialLevel, int moneyValue)
            {
                this.ID = new CraftingMaterialID(material);
                this.maxHeatLevel = maxHeatLevel;
                this.materialLevel = materialLevel;
                this.moneyValue = moneyValue;
                this.shaderPath = $"Materials/Metals/{material.ToString().Capitalize()}";
            }
            public override string GetName() => ID.ToString().Capitalize();
        }
        public static List<MetalData> metals = new List<MetalData>()
        {
            new MetalData(MetalMaterial.tin,        30, 1, 5),
            new MetalData(MetalMaterial.copper,     30, 1, 5),
            new MetalData(MetalMaterial.iron,       45, 2, 10),
            new MetalData(MetalMaterial.bronze,     45, 2, 10),
            new MetalData(MetalMaterial.silver,     60, 2, 20),
            new MetalData(MetalMaterial.steel,      60, 2, 30),
        };
        public static MaterialData FindMaterial(CraftingMaterialID ID)
        {
            switch (ID.type)
            {
                case CraftingMaterialType.metal:
                    return FindMetal((MetalMaterial)ID.subtype);
                case CraftingMaterialType.wood:
                case CraftingMaterialType.stone:
                    return null;
                default:
                    throw new System.ArgumentOutOfRangeException($"This CraftingMaterialType doesnt exist: {ID.type}");
            }
        }

        public static MetalData FindMetal(MetalMaterial material) => metals.Find(x => x.ID.subtype == (int)material);
    }
}