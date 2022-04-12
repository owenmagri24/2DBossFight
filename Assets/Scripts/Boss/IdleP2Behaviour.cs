using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IdleP2Behaviour : StateMachineBehaviour
{
    private BossController bossController;
    private ParticleSystem ps;
    float timer;
    float initialSpeed;
    float initialEmission;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent
        ps = bossController.GetRandomBossPs();
        if(!PhotonNetwork.IsMasterClient){ return; }

        initialSpeed = ps.main.simulationSpeed; //get speed of chosen particle system
        initialEmission = bossController.GetEmissionRate(ps); //get emission rate of chosen particle system

        bossController.ChangeParticleSpeed(bossController.ReturnWhichParticleSystem(ps), initialSpeed + 0.4f); //make particles faster
        bossController.ChangeEmissionRate(bossController.ReturnWhichParticleSystem(ps), initialEmission + 4f); //increase emissions

        bossController.PlayParticleSystem(bossController.ReturnWhichParticleSystem(ps));
        timer = 0f;//reset timer
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!PhotonNetwork.IsMasterClient){ return; }
        
        timer += Time.deltaTime;

        if(timer > 4f) //to change
        {
            //animator.SetTrigger("Run2");
            bossController.ChangeAnimation("Run2");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!PhotonNetwork.IsMasterClient){ return; }

        bossController.ChangeParticleSpeed(bossController.ReturnWhichParticleSystem(ps), initialSpeed); //reset particle speed
        bossController.ChangeEmissionRate(bossController.ReturnWhichParticleSystem(ps), initialEmission); //reset emission rate
        bossController.StopParticleSystem(bossController.ReturnWhichParticleSystem(ps));
    }
}
