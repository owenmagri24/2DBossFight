using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private BossController bossController;
    private ParticleSystemManager psManager;
    private ParticleSystem ps;
    float timer;
    float initialSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent
        psManager = animator.transform.parent.GetComponent<ParticleSystemManager>();

        ps = psManager.GetRandomBossParticleSystem();
        initialSpeed = ps.main.simulationSpeed; //get speed of chosen particle system
        psManager.ChangeParticleSpeed(ps, initialSpeed + 0.2f);

        psManager.PlayParticleSystem(psManager.ReturnWhichParticleSystem(ps)); //Plays ps in int format because of RPC method
        timer = 0f;//reset timer
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if(timer > 4f) //to change
        {
            animator.SetTrigger("Run");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        psManager.ResetParticleSpeed(ps, initialSpeed);
        ps.Stop();
    }
}
