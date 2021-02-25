using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PreventAttackingStateMachineBehaviour : StateMachineBehaviour
    {
        public bool startAtTime;
        public int startFrame;
        public float startTime => (float)startFrame / 60;
        public bool endAtTime;
        public int endFrame;
        public float endTime => (float)endFrame / 60;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!startAtTime)
                AnimationDataHandler.instance.preventAttacking = true;
            Debug.Log(endTime);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (startAtTime)
                if (Mathf.Abs(stateInfo.normalizedTime - startTime) <= 0.01f)
                    AnimationDataHandler.instance.preventAttacking = true;
            if (endAtTime)
                if (Mathf.Abs(stateInfo.normalizedTime - endTime) <= 0.01f)
                {
                    AnimationDataHandler.instance.preventAttacking = false;
                }
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AnimationDataHandler.instance.preventAttacking = false;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}