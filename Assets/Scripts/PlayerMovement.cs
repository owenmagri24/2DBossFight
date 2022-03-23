using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    Rigidbody2D rb;
    public float normalAcceleration;
    [HideInInspector] public float acceleration;
    [HideInInspector] public Vector2 movementInput;

    //Rotation and animation
    private Animator animator;
    public GameObject playerAttachments;
    [SerializeField] private GameObject playerSprites;
    private bool facingRight = true;

    

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        acceleration = normalAcceleration;
        Physics2D.IgnoreLayerCollision(3,7); //Ignores collision between layer 3 (Player) & layer 7 (Boss)
    }


    void FixedUpdate() 
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerAttachments.transform.up = (mousePos - (Vector2)transform.position).normalized; //rotate playerattachments

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity += movementInput * acceleration * Time.fixedDeltaTime;

        if(movementInput.x < 0 && facingRight)
        {
            //Facing left
            FlipSprites();
        }
        if(movementInput.x > 0 && !facingRight)
        {
            //Facing Right
            FlipSprites();
        }
        animator.SetFloat("Speed", movementInput.sqrMagnitude);//animation
        
    }

    void FlipSprites()
    {
        Vector3 currentScale = playerSprites.transform.localScale;
        currentScale.x *= -1;
        playerSprites.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
