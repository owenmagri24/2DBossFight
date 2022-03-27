using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private BossController bossController;
    private ParticleSystem ps;
    float timer;
    float initialSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent
        ps = ParticleSystemManager.instance.GetRandomBossParticleSystem(); //play random particle system
        initialSpeed = ps.main.simulationSpeed; //get speed of chosen particle system
        ParticleSystemManager.instance.ChangeParticleSpeed(ps, initialSpeed + 0.2f); //make particles faster

        ps.Play(); //start particle system
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
        ParticleSystemManager.instance.ResetParticleSpeed(ps, initialSpeed); //reset particle speed
        ps.Stop();
    }
}
