using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    private bool inSkillCheck;

    private void Start() {
        inSkillCheck = false;
    }
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(inSkillCheck)
            {
                //Succesful Skillcheck
                Debug.Log("Success");
            }
            else
            {
                //Bad Skillcheck
                Debug.Log("Failure");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Note")
        {
            inSkillCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Note")
        {
            inSkillCheck = false;
        }
    }
}
