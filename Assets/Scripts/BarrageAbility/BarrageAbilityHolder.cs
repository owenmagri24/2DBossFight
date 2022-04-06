using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageAbilityHolder : MonoBehaviour
{
    public BarrageAbility ability;
    [SerializeField] private GameObject skillCheck;
    [SerializeField] private ParticleSystem ps;
    private RotationCheck rotationCheck;
    private KeyCode key;
    private float cooldownTime;

    public enum AbilityState{
        ready,
        active,
        cooldown
    }

    [HideInInspector] public AbilityState state = AbilityState.ready;

    private void Awake() 
    {
        rotationCheck = FindObjectOfType<RotationCheck>();
    }

    private void Start() 
    {
        key = ability.key;
    }

    void Update()
    {
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
                        ps.Play(); //start particle system
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
