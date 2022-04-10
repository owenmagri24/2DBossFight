using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BarrageAbilityHolder : MonoBehaviour
{
    public BarrageAbility ability;
    private GameObject skillCheck;
    private RotationCheck rotationCheck;
    private KeyCode key;
    private float cooldownTime;
    private PhotonView photonView;

    public enum AbilityState{
        ready,
        active,
        cooldown
    }

    [HideInInspector] public AbilityState state = AbilityState.ready;

    private void Awake() 
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start() 
    {
        key = ability.key;
        skillCheck = AbilityManager.instance.barrageSkillChecks[0];
        rotationCheck = skillCheck.GetComponentInChildren<RotationCheck>();
    }

    void Update()
    {
        if(!photonView.IsMine){ return; }

        switch (state)
        {
            case AbilityState.ready://If state is ready
                if(Input.GetKeyDown(key)) //Player presses ability key
                {
                    skillCheck.SetActive(true);
                    ability.Activate(gameObject);
                    state = AbilityState.active; //set state to active
                }
            break;

            case AbilityState.active:
                if(Input.GetKeyDown(key))
                {
                    if(rotationCheck.inCheck)
                    {
                        //rotation check hit
                        gameObject.GetComponent<PlayerParticleSystem>().PlayParticleSystem(2); //start barrage ps
                    }
                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;  //unfreeze player
                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    skillCheck.SetActive(false); // turn off skillcheck

                    cooldownTime = ability.cooldownTime;
                    state = AbilityState.cooldown;
                }
            break;

            case AbilityState.cooldown:
                if(cooldownTime > 0){
                    cooldownTime -= Time.deltaTime;
                }
                else{ //when ability cooldowntime is ready
                    state = AbilityState.ready; //set state to active
                }
            break;
        }
    }
}
