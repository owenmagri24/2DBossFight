using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float noteSpeed;
    Rigidbody2D rb;

    public Vector2 notePos;
    
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        notePos = gameObject.transform.position;
    }

    private void FixedUpdate() {
        rb.velocity = Vector2.left * noteSpeed;

        if(gameObject.transform.position.x < -10f) //to change
        {
            Destroy(this.gameObject);
        }
    }
    
}
