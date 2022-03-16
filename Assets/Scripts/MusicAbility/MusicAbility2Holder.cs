using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbility2Holder : MonoBehaviour
{
    public MusicAbility ability;
    float cooldownTime;
    float startingCoolDownTime;
    KeyCode key;

    public GameObject[] playerPressers;
    private int whichPresser;

    public enum AbilityState{
        ready,
        active,
        cooldown
    }

    [HideInInspector] public AbilityState state = AbilityState.ready;

    private void Start() {
        key = ability.key;

    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                //If state is ready
                if(Input.GetKeyDown(key)) //Player presses ability key
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active; //set state to active
                }
                if(!playerPressers[0].activeSelf) //if playerpresser0 is not active
                {
                    whichPresser = 0;//put this in abilitymanager
                }
                else
                {
                    whichPresser = 1;
                }
            break;

            case AbilityState.active:
                if(!playerPressers[0].activeSelf) //if playerpresser0 is not active
                {
                    whichPresser = 0;
                }
                else
                {
                    whichPresser = 1;
                }

                playerPressers[whichPresser].SetActive(true); //turn on playerpress depending on whichpresser 
                
                if(ability.spawningReady) //when spawning finishes
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                    startingCoolDownTime = cooldownTime;
                }
            break;

            case AbilityState.cooldown:
                if(cooldownTime > 0){
                    cooldownTime -= Time.deltaTime;
                    if(cooldownTime < startingCoolDownTime - 1.5f && playerPressers[whichPresser].activeSelf) //1second after cooldown starts and playerpress is active
                    {
                        playerPressers[whichPresser].SetActive(false);
                    }
                }
                else{ //when ability cooldowntime is ready
                    state = AbilityState.ready; //set state to active
                }
            break;
        }
    }
}
