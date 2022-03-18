using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillCheck : MonoBehaviour
{

    public KeyCode presserKey;
    public bool inSkillCheck;
    private NoteScript note;
    

    private void Update() {
        if(Input.GetKeyDown(presserKey))
        {
            if(inSkillCheck)
            {
                //note hit
                if(note != null){ note.NoteDestroyAnimation(); }
                
            }
            else
            {
                //note missed
            } 
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Note")
        {
            note = other.GetComponent<NoteScript>();
        }
    }
}
