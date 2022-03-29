using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCheck : MonoBehaviour
{
    public bool inCheck;

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
