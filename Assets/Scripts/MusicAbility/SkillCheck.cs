using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillCheck : MonoBehaviour
{
    public KeyCode presserKey;
    public bool inSkillCheck;
    private NoteScript note;

    private void Awake() {
        
    }
    

    private void Update() {
        if(Input.GetKeyDown(presserKey))
        {
            if(inSkillCheck)
            {
                //note hit
                if(note != null) note.NoteDestroyAnimation();
                AbilityManager.instance.notesList.Remove(note);
                note.PlayAbilityPs();
            }
            else
            {
                //note missed
                AbilityManager.instance.IncreaseNoteSpeed(12f);
            } 
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Note" || other.tag == "Note2")
        {
            note = other.GetComponent<NoteScript>();
        }
    }
}
