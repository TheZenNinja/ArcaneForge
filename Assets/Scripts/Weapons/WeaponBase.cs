using System.Collections;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [HideInInspector]
        public Animator handAnim;
        [HideInInspector]
        public Transform rHand, lHand;
        [HideInInspector]
        public abstract WeaponType weaponType { get; }
        public GameObject mesh;
        public abstract void HandleInput();
        public abstract void UpdateUI();
        public virtual void Equip(Player.EquipmentController equipment)
        {
            rHand = equipment.weaponR;
            lHand = equipment.weaponL;
            gameObject.SetActive(true);
            handAnim = equipment.handAnim;
        }
        public virtual void Unequip()
        {
            gameObject.SetActive(false);
        }
        public void ToggleVisibility(bool visible = true)
        {
            mesh.SetActive(visible);
            StaticRefences.equipmentUI.EquipWeapon(weaponType);
        }
    }
}