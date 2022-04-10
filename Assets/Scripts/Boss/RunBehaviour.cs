using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviour : StateMachineBehaviour
{
    private BossController bossController;
    private ParticleSystemManager psManager;
    private ParticleSystem ps;
    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent
        psManager = animator.transform.parent.GetComponent<ParticleSystemManager>();
        ps = psManager.GetRandomBossParticleSystem();
        psManager.PlayParticleSystem(psManager.ReturnWhichParticleSystem(ps)); //Plays ps in int format because of RPC method
        timer = 0f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.BossMove();

        timer += Time.deltaTime;

        if(timer > 4f) //to change
        {
            animator.SetTrigger("Idle");
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ps.Stop();
    }
}
