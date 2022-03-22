using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehaviour : StateMachineBehaviour
{

    private Cinemachine.CinemachineTargetGroup targetGroup;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetGroup = GameObject.FindWithTag("MainCamera").GetComponent<Cinemachine.CinemachineTargetGroup>(); //get target group
        
        animator.SetTrigger("Idle");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime > .3f) //3/10 of the animation done
        {
            if(targetGroup != null)
            {
                targetGroup.m_Targets[0].weight = 0; //Change player weight to 0 (Target boss)
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetGroup.m_Targets[0].weight = 1; // Change camera to normal
    }
}
