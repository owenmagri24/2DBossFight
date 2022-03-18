using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillCheck : MonoBehaviour
{

    public enum ControlKey
    {
        E = KeyCode.E,
        Q = KeyCode.Q
    }

    private bool note1inSkillCheck;
    private bool note2inSkillCheck;

    public ControlKey key;

    // public UnityEvent OnKeyPress = new UnityEvent();

    private void Start() {
        note1inSkillCheck = false;
        note2inSkillCheck = false;

        // OnKeyPress.AddListener(PressedKey);
        // OnKeyPress.Invoke();
    }

    // void PressedKey()
    // {
    //     Debug.Log("Hurrah1");
    // }
    
    private void Update() {
        if(Input.GetKeyDown((KeyCode) (int) key))
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        note1inSkillCheck = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        note1inSkillCheck = false;
    }
}
