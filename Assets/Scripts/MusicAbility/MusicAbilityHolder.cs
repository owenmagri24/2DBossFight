using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbilityHolder : MonoBehaviour
{
    public MusicAbility ability;
    float cooldownTime;
    float startingCoolDownTime;
    KeyCode key;

    public GameObject playerPress;

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
            break;

            case AbilityState.active:
                playerPress.SetActive(true); //turn on playerpress visual
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
                    if(cooldownTime < startingCoolDownTime - 1.5f && playerPress.activeSelf) //1second after cooldown starts and playerpress is active
                    {
                        playerPress.SetActive(false);
                    }
                }
                else{ //when ability cooldowntime is ready
                    state = AbilityState.ready; //set state to active
                }
            break;
        }
    }
}
