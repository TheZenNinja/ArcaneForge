using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Crafting;

namespace Inventory
{
    public class WeaponItem : Item
    {
        public WeaponType type;


        public GameObject GetObject()
        {
            return Resources.Load<GameObject>($"Objects/Weapons/{type.ToString().Capitalize()}");
        }
        public GameObject GetWeapon()
        {
            return Resources.Load<GameObject>($"Weapons/{type.ToString().Capitalize()}");
        }
    }
}
