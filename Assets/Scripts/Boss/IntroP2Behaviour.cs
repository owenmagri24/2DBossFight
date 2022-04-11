using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IntroP2Behaviour : StateMachineBehaviour
{

    private Cinemachine.CinemachineTargetGroup targetGroup;
    private int rand;
    private BossController bossController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetGroup = GameObject.Find("TargetGroup").GetComponent<Cinemachine.CinemachineTargetGroup>(); //get target group
        animator.transform.parent.GetComponent<CapsuleCollider2D>().enabled = false; //disable capsule collider for intro animation
        bossController = animator.transform.parent.GetComponent<BossController>();

        if(!PhotonNetwork.IsMasterClient){ return; }

        rand = Random.Range(0,2); //choose random state

        if(rand == 0)
        {
            bossController.ChangeAnimation("Idle2");
        }
        else
            bossController.ChangeAnimation("Run2");
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime < .7f) // 7/10 of the animation done
        {
            if(targetGroup != null)
            {
                targetGroup.m_Targets[1].weight = 0; //Change player weight to 0 (Target boss)
            }
        }
        else
        {
            targetGroup.m_Targets[1].weight = 1; // Change camera to normal
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.GetComponent<CapsuleCollider2D>().enabled = true; //enable capsule collider
    }
}
