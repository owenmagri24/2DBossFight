using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    Rigidbody2D rb;
    public float startingSpeed;
    [HideInInspector] public float activeSpeed;
    [HideInInspector] public Vector2 movementInput;

    //Rotation and animation
    private Animator animator;
    public GameObject playerAttachments;
    [SerializeField] private GameObject playerSprites;
    private bool facingRight = true;

    //Boundary
    private Vector2 mapBounds;

    //Health
    [SerializeField] private float startingHealth;
    [HideInInspector] public float health;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        health = startingHealth;
        activeSpeed = startingSpeed;
        Physics2D.IgnoreLayerCollision(3,7); //Ignores collision between layer 3 (Player) & layer 7 (Boss)
        mapBounds = new Vector3(12, 9, Camera.main.transform.position.z);
    }


    void FixedUpdate() 
    {
        Movement();
    }

    private void LateUpdate() //Boundary check works smoother in late update
    {
        Boundaries();
    }

    void Movement()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerAttachments.transform.up = (mousePos - (Vector2)transform.position).normalized; //rotate playerattachments

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movementInput * activeSpeed;

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

    void Boundaries()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, mapBounds.x * -1, mapBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, mapBounds.y * -1, mapBounds.y);
        transform.position = viewPos;
    }

    void FlipSprites()
    {
        Vector3 currentScale = playerSprites.transform.localScale;
        currentScale.x *= -1;
        playerSprites.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void PlayerHit(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hit");
    }
}
