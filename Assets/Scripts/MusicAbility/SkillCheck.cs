using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    private bool note1inSkillCheck;
    private bool note2inSkillCheck;

    private void Start() {
        note1inSkillCheck = false;
        note2inSkillCheck = false;
    }
    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(note1inSkillCheck)
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(note2inSkillCheck)
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
            if(other.transform.position.y == gameObject.transform.position.y)
            {
                note1inSkillCheck = true;
            }
        }
        else if(other.tag == "Note2")
        {
            if(other.transform.position.y == gameObject.transform.position.y)
            {
                note2inSkillCheck = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Note")
        {
            if(other.transform.position.y == gameObject.transform.position.y)
            {
                note1inSkillCheck = false;
            }
        }
        else if(other.tag == "Note2")
        {
            if(other.transform.position.y == gameObject.transform.position.y)
            {
                note2inSkillCheck = false;
            }
        }
    }
}
