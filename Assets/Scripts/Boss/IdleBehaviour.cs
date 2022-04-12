using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        ps = bossController.GetRandomBossPs();
        if(!PhotonNetwork.IsMasterClient){ return; }

        initialSpeed = ps.main.simulationSpeed; //get speed of chosen particle system
        bossController.ChangeParticleSpeed(bossController.ReturnWhichParticleSystem(ps), initialSpeed + 0.2f);
        bossController.PlayParticleSystem(bossController.ReturnWhichParticleSystem(ps)); //Plays ps in int format because of RPC method
        timer = 0f;//reset timer
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!PhotonNetwork.IsMasterClient){ return; }
        timer += Time.deltaTime;

        if(timer > 4f) //to change
        {
            //animator.SetTrigger("Run");
            bossController.ChangeAnimation("Run");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if(!PhotonNetwork.IsMasterClient){ return; }
        bossController.ChangeParticleSpeed(bossController.ReturnWhichParticleSystem(ps), initialSpeed);
        bossController.StopParticleSystem(bossController.ReturnWhichParticleSystem(ps));
    }

    
}
