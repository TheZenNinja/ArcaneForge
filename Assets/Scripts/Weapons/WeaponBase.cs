using System.Collections;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public Animator handAnim;

        public abstract void HandleInput();
        public virtual void Equip(Player.EquipmentController equipment)
        {
            gameObject.SetActive(true);
            handAnim = equipment.handAnim;
        }
        public virtual void Unequip()
        {
            gameObject.SetActive(false);
        }

    }
}