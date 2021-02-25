using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Crafting;

namespace Weapons
{
    [System.Serializable]
    public class WeaponData
    {
        public enum EquipedHand
        { 
            right = 0,
            left = 1,
        }


        public WeaponType type;
        public EquipedHand hand;
        [SerializeField]
        public List<PartData> parts;
        //list of enums for attributes like +monster dmg and then the bonus dmg is calculated based on how many times it appears

        public GameObject GetObject() => Resources.Load<GameObject>($"Objects/Weapons/{type.ToString().Capitalize()}");
        public GameObject GetWeapon() => Resources.Load<GameObject>($"Weapons/{type.ToString().Capitalize()}");
    }
}
