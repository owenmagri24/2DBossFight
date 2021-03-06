using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbilityHolder : MonoBehaviour
{
    public MusicAbility ability;
    float cooldownTime;
    KeyCode key;
    GameObject whichPresser;

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
            case AbilityState.ready://If state is ready
                if(Input.GetKeyDown(key)) //Player presses ability key
                {
                    ability.Activate(gameObject);
                    whichPresser = AbilityManager.instance.checkWhichPresser();
                    whichPresser.SetActive(true);
                    whichPresser.GetComponent<SkillCheck>().presserKey = key;

                    state = AbilityState.active; //set state to active
                }
            break;

            case AbilityState.active:
                if(ability.spawningReady) //when spawning finishes
                {
                    cooldownTime = ability.cooldownTime;
                    AbilityManager.instance.TurnOffPresser(whichPresser, cooldownTime - 2.8f); //turn off presser and usingspawner to false after delay
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
