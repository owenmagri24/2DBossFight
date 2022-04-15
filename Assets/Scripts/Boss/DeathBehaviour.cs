using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour
{
    private BossController bossController;
    private PlayerMovement[] playersScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playersScript = FindObjectsOfType<PlayerMovement>();
        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent

        for (int i = 0; i < playersScript.Length; i++)
        {
            playersScript[i].BossDie();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 1) //state finished
        {
            bossController.Death();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
