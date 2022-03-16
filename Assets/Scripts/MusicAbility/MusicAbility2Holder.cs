using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbility2Holder : MonoBehaviour
{
    public MusicAbility ability;
    float cooldownTime;
    float startingCoolDownTime;
    KeyCode key;
    GameObject whichPresser;

    public GameObject[] playerPressers;

    public NoteScript noteScript;

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
                    whichPresser = checkWhichPresser();
                    if(!playerPressers[0].activeSelf) //if playerpresser0 is not active
                    {
                        playerPressers[0].SetActive(true);
                    }
                    else
                    {
                        playerPressers[1].SetActive(true);
                    }
                    state = AbilityState.active; //set state to active
                }
            break;

            case AbilityState.active:
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

                    if(cooldownTime < startingCoolDownTime - 1.5f) //1second after cooldown starts
                    {
                        whichPresser.SetActive(false);
                    }
                }
                else{ //when ability cooldowntime is ready
                    state = AbilityState.ready; //set state to active
                }
            break;
        }
    }

    private GameObject checkWhichPresser()
    {
        for (int i = 0; i < playerPressers.Length; i++)
        {
            Debug.Log("Note Pos: "+ ability.note.transform.position.y + " . Presser pos: "+ playerPressers[i].transform.position.y);
            if(ability.note.transform.position.y == playerPressers[i].transform.position.y)
            {
                return playerPressers[i];
            }
        }
        Debug.Log("return null");
        return null;
    }

}
