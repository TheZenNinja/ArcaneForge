using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Player.UI;

namespace Player
{
    public class EquipmentController : MonoBehaviour
    {
        [System.Serializable]
        public class EquipmentSet
        {
            [Header("Data")]
            [SerializeReference]
            public WeaponData primary = WeaponData.none;
            public WeaponData secondary = WeaponData.none;

            [Header("GameObjects")]
            public WeaponBase primaryObject;
            public WeaponBase secondaryObject;

            public void EnableSelectedWeapon(WeaponBase currentWeapon)
            {
                primaryObject.gameObject.SetActive(primaryObject == currentWeapon);
                secondaryObject.gameObject.SetActive(primaryObject == currentWeapon);
            }
            public void InstantiateMeshes(Transform R, Transform L)
            {
                if (primary.hand == WeaponData.EquipedHand.right)
                    primaryObject = Instantiate(primary.GetWeapon(), R).GetComponent<WeaponBase>();
                else
                    primaryObject = Instantiate(primary.GetWeapon(), L).GetComponent<WeaponBase>();

                if (secondary.hand == WeaponData.EquipedHand.right)
                    secondaryObject = Instantiate(secondary.GetWeapon(), R).GetComponent<WeaponBase>();
                else
                    secondaryObject = Instantiate(secondary.GetWeapon(), L).GetComponent<WeaponBase>();
            }
            public void DestroyGameObjects()
            {
                if (primaryObject.gameObject)
                {
                    if (Application.isPlaying)
                        Destroy(primaryObject.gameObject);
                    else
                        DestroyImmediate(primaryObject.gameObject);
                }

                if (secondaryObject.gameObject)
                {
                    if (Application.isPlaying)
                        Destroy(secondaryObject.gameObject);
                    else
                        DestroyImmediate(secondaryObject.gameObject);
                }
            }
            ~EquipmentSet()
            {
                DestroyGameObjects();
            }
            
            //[SerializeReference]
            //public WeaponData ability1, ability2;
        }


        public EquipmentUI UI;
        public Animator handAnim;

        public EquipmentSet primaryEquipment;
        public EquipmentSet secondaryEquipment;

        //[SerializeReference] 
        //public List<WeaponData> weapons;
        //
        //public List<WeaponBase> weaponObjects;
        public bool usingSecondaryEquip;
        public bool usingSecondaryWep;
        public Transform weaponR, weaponL;

        public WeaponBase currentWeapon => GetCurrentWeaponObj();

        #region Debugging
        [Header("Editor Debugging")]
        [SerializeField] private WeaponSO editorWeapon;
        [SerializeField] private bool editorIsSecondaryEquipment;
        [SerializeField] private bool editorIsSecondaryWeapon;
        [ContextMenu("Add Weapon To Equipment")]
        private void AddWeaponInEditor()
        {
            if (editorWeapon == null)
                return;
            if (editorIsSecondaryEquipment)
            {
                if (editorIsSecondaryWeapon)
                    secondaryEquipment.secondary = editorWeapon.data;
                else
                    secondaryEquipment.primary = editorWeapon.data;
            }
            else
            {
                if (editorIsSecondaryWeapon)
                    primaryEquipment.secondary = editorWeapon.data;
                else
                    primaryEquipment.primary = editorWeapon.data;
            }
        }
        #endregion
        private void Start()
        {
            ReloadEquipment();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                Debug.Log(GetCurrentWeaponObj<RangedWeapon>());


            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwapEquipment();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwapWeapon();

            if (!StaticRefences.animDataHandler.preventAttacking)
            {
                currentWeapon.HandleInput();
                currentWeapon.UpdateUI();
            }
        }
        public WeaponBase GetCurrentWeaponObj()
        {
            if (usingSecondaryEquip)
                return usingSecondaryWep ? secondaryEquipment.secondaryObject : secondaryEquipment.primaryObject;
            else
                return usingSecondaryWep ? primaryEquipment.secondaryObject : primaryEquipment.primaryObject;
        }  
        public T GetCurrentWeaponObj<T>() where T : WeaponBase
        {
            WeaponBase wep = GetCurrentWeaponObj();
            try
            {
                return (T)wep;
            }
            catch (System.InvalidCastException ex)
            {
                Debug.Log("Invalid Cast");
                return null;
            }
        }
        public void SwapEquipment()
        {
            usingSecondaryEquip = !usingSecondaryEquip;
            UpdateSelectedWeapon();
        }
        public void SwapWeapon()
        {
            usingSecondaryWep = !usingSecondaryWep;
            UpdateSelectedWeapon();
        }
        public void UpdateSelectedWeapon()
        {
            primaryEquipment.EnableSelectedWeapon(currentWeapon);
            secondaryEquipment.EnableSelectedWeapon(currentWeapon);
        }

        public void ReloadEquipment()
        {
            primaryEquipment.DestroyGameObjects();
            primaryEquipment.InstantiateMeshes(weaponR, weaponL);

            secondaryEquipment.DestroyGameObjects();
            secondaryEquipment.InstantiateMeshes(weaponR, weaponL);
        }

        public void ToggleWeaponVisibility(bool visible = true) => currentWeapon.ToggleVisibility(visible);
        public void PassDataToWeapon(WeaponBase.AnimDataStatePass data) => currentWeapon.ApplyDataInfo(data);

    }
}