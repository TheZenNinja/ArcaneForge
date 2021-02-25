using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Player.UI;

namespace Player
{
    public class EquipmentController : MonoBehaviour
    {
        public EquipmentUI UI;
        public Animator handAnim;
        [SerializeReference] 
        public List<WeaponData> weapons;

        public List<WeaponBase> weaponObjects;
        public int weaponIndex;
        public Transform weaponR, weaponL;
        #region Debugging
        [Header("Editor Debugging")]
        [SerializeField] private WeaponSO editorWeapon1;
        [SerializeField] private WeaponSO editorWeapon2;
        [SerializeField] private WeaponSO editorWeapon3;
        [ContextMenu("Add Weapon To Equipment")]
        private void AddWeaponInEditor()
        {
            EquipItem(editorWeapon1.data, 0);
            EquipItem(editorWeapon2.data, 1);
            EquipItem(editorWeapon3.data, 2);
        }
        #endregion
        private void Start()
        {
            Equip(0);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Equip(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Equip(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Equip(2);

            if (!AnimationDataHandler.instance.preventAttacking)
                weaponObjects[weaponIndex].HandleInput();
        }
        public void Equip(int index)
        {
            weaponIndex = index;
            handAnim.SetInteger("Weapon Type", (int)weapons[index].type);

            for (int i = 0; i < weaponObjects.Count; i++)
            {
                if (index == i)
                {
                    if (!weaponObjects[i])
                        CreateWeaponMesh(weapons[i], i);

                    weaponObjects[i].Equip(this);
                }
                else if (weaponObjects[i])
                    weaponObjects[i].Unequip();
            }
        }
        /// <param name="index"> 3 > index >= 0 </param>
        public void EquipItem(WeaponData data, int index)
        {
            weapons[index] = data;
            if (weaponObjects[index].gameObject)
                Destroy(weaponObjects[index].gameObject);

            CreateWeaponMesh(data, index);
        }
        public void CreateWeaponMesh(WeaponData data, int index)
        {
            WeaponBase w;

            switch (data.hand)
            {
                case WeaponData.EquipedHand.left:
                    w = Instantiate(data.GetWeapon(), weaponL).GetComponent<WeaponBase>();
                    break;
                case WeaponData.EquipedHand.right:
                default:
                    w = Instantiate(data.GetWeapon(), weaponR).GetComponent<WeaponBase>();
                    break;
            }
            weaponObjects[index] = w;
        }
    }
}