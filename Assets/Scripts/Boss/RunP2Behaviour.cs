using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunP2Behaviour : StateMachineBehaviour
{
    private BossController bossController;
    private ParticleSystem ps;
    float timer;
    float initialSpeed;
    float initialEmission;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.transform.parent.GetComponent<BossController>(); //get bosscontroller from parent
        ps = ParticleSystemManager.instance.GetRandomBossParticleSystem(); //play random particle system

        initialSpeed = ps.main.simulationSpeed; //get speed of chosen particle system
        initialEmission = ParticleSystemManager.instance.GetEmissionRate(ps); //get emission rate of ps

        ParticleSystemManager.instance.ChangeParticleSpeed(ps, initialSpeed + 0.2f); //make particles faster
        ParticleSystemManager.instance.ChangeEmissionRate(ps, initialEmission + 2f); //increase emission rate
        bossController.moveSpeed += 2f; //make boss move faster

        ps.Play(); //start particle system
        timer = 0f;//reset timer
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.BossMove(); //need to increase speed

        timer += Time.deltaTime;

        if(timer > 4f) //to change
        {
            animator.SetTrigger("Idle2");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParticleSystemManager.instance.ResetParticleSpeed(ps, initialSpeed); //reset particle speed
        ParticleSystemManager.instance.ChangeEmissionRate(ps, initialEmission); //reset emission rate
        bossController.moveSpeed -= 2f; //reset boss speed
        ps.Stop();
    }
}
