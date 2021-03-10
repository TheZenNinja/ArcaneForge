using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Player.UI
{
    public class EquipmentUI : MonoBehaviour
    {
        public Image cursorDot;
        public Image cursorCircle;

        [Header("Ammo")]
        public GameObject ammoContainer;
        public TextMeshProUGUI ammoTxt;
        private void Start()
        {
            StaticRefences.equipmentUI = this;
        }
        public void EquipWeapon(WeaponType type)
        {
            HideAllUI();
            switch (type)
            {
                case WeaponType.bow:
                    cursorCircle.gameObject.SetActive(true);
                    break;
                case WeaponType.longbow:
                    cursorCircle.gameObject.SetActive(true);
                    break;
                case WeaponType.crossbow:
                    ammoContainer.SetActive(true);
                    break;
                case WeaponType.shuriken:
                    ammoContainer.SetActive(true);
                    break;
                case WeaponType.kunai:
                    ammoContainer.SetActive(true);
                    break;
                case WeaponType.gun:
                    ammoContainer.SetActive(true);
                    break;
                case WeaponType.sword:
                case WeaponType.dagger:
                case WeaponType.greatsword:
                case WeaponType.katana:
                case WeaponType.spear:
                case WeaponType.staff:
                default:
                    Debug.LogWarning($"Weapon type \"{type}\" not implimented");
                    break;
            }
        }
        public bool UpdateAmmoToReloading()
        {
            if (ammoContainer.activeSelf)
            {
                ammoTxt.text = "-/-";
                return true;
            }
            return false;
        }
        public bool UpdateAmmo(Weapons.AmmoCounter ammo)
        {
            if (ammoContainer.activeSelf)
            {
                ammoTxt.text = ammo.ToString();
                return true;
            }
            return false;
        }
        public void HideAllUI()
        {
            ammoContainer.SetActive(false);
            ammoContainer.gameObject.SetActive(false);
        }
    }
}