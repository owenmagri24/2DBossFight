using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float normalAcceleration;
    public GameObject playerAttachments;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = normalAcceleration;
    }


    void FixedUpdate() 
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerAttachments.transform.up = (mousePos - (Vector2)transform.position).normalized; //rotate playerattachments

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity += movementInput * acceleration * Time.fixedDeltaTime;

    }
}
