using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public AbilityBase ability;
    float cooldownTime;
    float activeTime;
    KeyCode key;

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
                    ability.Activate(gameObject); //call ability function
                    state = AbilityState.active; //set state to active
                    activeTime = ability.activeTime; //set activetime to ability activetime
                }
            break;

            case AbilityState.active:
                if(activeTime > 0){
                    activeTime -= Time.deltaTime;
                }
                else{ //when active time is ready
                    state = AbilityState.cooldown; //set state to cooldown
                    cooldownTime = ability.cooldownTime; //set cooldoowntime to ability cooldowntime
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
