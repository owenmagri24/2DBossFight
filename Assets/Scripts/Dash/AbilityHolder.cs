using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    public AbilityBase ability;
    private float cooldownTime;
    private float activeTime;
    private KeyCode key;
    private Collider2D coll;

    public enum AbilityState{
        ready,
        active,
        cooldown
    }

    [HideInInspector] public AbilityState state = AbilityState.ready;

    private void Start() {
        key = ability.key;
        coll = gameObject.GetComponent<Collider2D>();
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
                    coll.enabled = false; //disable player collider
                    activeTime -= Time.deltaTime;
                }
                else{ //when active time is ready
                    playerMovement.activeSpeed = playerMovement.startingSpeed; //reset speed
                    coll.enabled = true; //enable player collider
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
