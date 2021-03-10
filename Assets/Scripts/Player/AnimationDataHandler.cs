using System.Collections;
using UnityEngine;

namespace Player
{
    public class AnimationDataHandler : MonoBehaviour
    {
        private void Awake() => StaticRefences.animDataHandler = this;

        public bool preventAttacking;
        public Animator anim;
        public EquipmentController equipment;

        public void ToggleWeaponVisibility(int visible)
        {
            equipment.ToggleWeaponVisibility(visible == 1);
        }
    }
}