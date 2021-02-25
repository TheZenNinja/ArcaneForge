using System.Collections;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Testing/Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public WeaponData data;
    }
}