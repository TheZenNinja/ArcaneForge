using System.Collections;
using UnityEngine;

namespace Player
{
    public class AnimationDataHandler : MonoBehaviour
    {
        public static AnimationDataHandler instance;
        private void Awake() => instance = this;

        public bool preventAttacking;
        public Animator anim;
    }
}