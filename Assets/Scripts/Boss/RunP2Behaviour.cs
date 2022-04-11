using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RunP2Behaviour : StateMachineBehaviour
{
    private BossController bossController;
    private ParticleSystem ps;
    float timer;
    float initialSpeed;
    float initialEmission;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!PhotonNetwork.IsMasterClient){ return; }

        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent
        ps = bossController.GetRandomBossPs();

        initialSpeed = ps.main.simulationSpeed; //get speed of chosen particle system
        initialEmission = bossController.GetEmissionRate(ps); //get emission rate of ps

        bossController.ChangeParticleSpeed(bossController.ReturnWhichParticleSystem(ps), initialSpeed + 0.2f); //make particles faster
        bossController.ChangeEmissionRate(bossController.ReturnWhichParticleSystem(ps), initialEmission + 2f); //increase emission rate
        bossController.moveSpeed += 2f; //make boss move faster

        bossController.PlayParticleSystem(bossController.ReturnWhichParticleSystem(ps));
        timer = 0f;//reset timer
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!PhotonNetwork.IsMasterClient){ return; }

        bossController.BossMove(); //need to increase speed

        timer += Time.deltaTime;

        if(timer > 4f) //to change
        {
            bossController.ChangeAnimation("Idle2");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!PhotonNetwork.IsMasterClient){ return; }

        bossController.ChangeParticleSpeed(bossController.ReturnWhichParticleSystem(ps), initialSpeed); //reset particle speed
        bossController.ChangeEmissionRate(bossController.ReturnWhichParticleSystem(ps), initialEmission); //reset emission rate
        bossController.moveSpeed -= 2f; //reset boss speed
        bossController.StopParticleSystem(bossController.ReturnWhichParticleSystem(ps));
    }
}
