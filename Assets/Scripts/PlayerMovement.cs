using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    Rigidbody2D rb;
    public float startingSpeed;
    [HideInInspector] public float activeSpeed;
    [HideInInspector] public Vector2 movementInput;

    //Shooting
    private float canFireTime;
    [SerializeField] private float startCanFireTime;

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

    //Networking
    private PhotonView photonView;
    [SerializeField] private GameObject whiteArrow;
    
    //ParticleSystems;
    [SerializeField] private ParticleSystem shootingPS;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(photonView.IsMine)
        {
            whiteArrow.SetActive(true); //Can only see your white arrow
        }
        health = startingHealth;
        activeSpeed = startingSpeed;
        Physics2D.IgnoreLayerCollision(3,7); //Ignores collision between layer 3 (Player) & layer 7 (Boss)
        mapBounds = new Vector3(12, 9, Camera.main.transform.position.z);
    }

    private void Update() 
    {
        if(photonView.IsMine)
        {
            Shooting();
            LookDirection();
        }
    }

    void FixedUpdate() 
    {
        if(photonView.IsMine)
        {
            Movement();
        }
    }

    private void LateUpdate() //Boundary check works smoother in late update
    {
        Boundaries();
    }

    void Movement()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movementInput * activeSpeed;

        //Movement
        if(movementInput.x < 0 && facingRight)
        {
            //Facing left
            photonView.RPC("FlipSprites", RpcTarget.All);
        }
        if(movementInput.x > 0 && !facingRight)
        {
            //Facing Right
            photonView.RPC("FlipSprites", RpcTarget.All);
        }
        animator.SetFloat("Speed", movementInput.sqrMagnitude);//animation
    }

    void LookDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerAttachments.transform.up = (mousePos - (Vector2)transform.position).normalized; //rotate playerattachments
    }

    void Shooting()
    {
        if(canFireTime <= 0) // if can fire
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                photonView.RPC("RPC_Shoot", RpcTarget.All);
                canFireTime = startCanFireTime; //Reset CanFire timer
            }
        }
        else
        {
            canFireTime -= Time.deltaTime; //reduce canfiretime per second
        }
    }

    [PunRPC] void RPC_Shoot() //Gets called on all instances of the photon viewID
    {
        shootingPS.Play(); //shoot particle
    }

    void Boundaries()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, mapBounds.x * -1, mapBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, mapBounds.y * -1, mapBounds.y);
        transform.position = viewPos;
    }

    [PunRPC] 
    void FlipSprites()
    {
        Vector3 currentScale = playerSprites.transform.localScale;
        currentScale.x *= -1;
        playerSprites.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void PlayerHit(float damage)
    {
        if(!photonView.IsMine){ return; }

        health -= damage;
        animator.SetTrigger("Hit");
        if(health <= 0)
        {
            //Dead
        }
    }
}
