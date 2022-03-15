using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCheck : MonoBehaviour
{
    public FreezeMechanic freezeMechanic;

    private bool inCheck;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && inCheck)
        {
            freezeMechanic.MechanicOff();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "PlayerSkillCheck")
        {
            inCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "PlayerSkillCheck")
        {
            inCheck = false;
        }
    }
}
