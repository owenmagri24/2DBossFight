using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance; //instance created to call coroutine in MusicAbility script

    Rigidbody2D rb;
    public float normalAcceleration;
    public GameObject playerAttachments;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        acceleration = normalAcceleration;
        PlayerMovement.instance = this;
    }


    void FixedUpdate() 
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerAttachments.transform.up = (mousePos - (Vector2)transform.position).normalized; //rotate playerattachments

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity += movementInput * acceleration * Time.fixedDeltaTime;

    }
}
