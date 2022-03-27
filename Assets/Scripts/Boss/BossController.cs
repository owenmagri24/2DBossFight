using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private float startingHealth;
    [HideInInspector] public float health;
    private bool moveRight;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Awake() {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }
    
    private void Start() {
        moveRight = true;
        health = startingHealth;
    }

    private void Update() {
        if(health <= startingHealth/2) //half hp
        {
            anim.SetTrigger("Phase2");
        }
        if(health <= 0)
        {
            anim.SetTrigger("Death");
        }
    }
    

    public void BossMove() // Boss moves left and right
    {
        if(transform.position.x > 7f)
        {
            moveRight = false;
            spriteRenderer.flipX = true;
        }
        else if(transform.position.x < -7f)
        {
            moveRight = true;
            spriteRenderer.flipX = false;
        }

        if(moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }
}
