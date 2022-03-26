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
    public ParticleSystem[] particleSystems;

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

    public ParticleSystem GetRandomParticleSystem()
    {
        int rand = Random.Range(0, particleSystems.Length);
        return particleSystems[rand];
    }

    public void ChangeParticleSpeed(ParticleSystem ps, float value)
    {
        var main = ps.main;
        main.simulationSpeed = value;
    }

    public void ResetParticleSpeed(ParticleSystem ps,float value)
    {
        var main = ps.main;
        main.simulationSpeed = value;
    }

    public void ChangeEmissionRate(ParticleSystem ps, float value)
    {
        var emission = ps.emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            emission.SetBurst(0,
            new ParticleSystem.Burst(0, value)); //set burst to burstcount +value
        }
        else
        {
            emission.rateOverTime = value;
        }
    }

    public float GetEmissionRate(ParticleSystem ps)
    {
        var emission = ps.emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            var burst = emission.GetBurst(0).count;
            return burst.constant;
        }
        else
            return emission.rateOverTime.constant;
    }

    public void ResetEmissionRate(ParticleSystem ps, float value)
    {
        var emission = ps.emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            emission.SetBurst(0,
            new ParticleSystem.Burst(0, value)); //set burst to burstcount +value
        }
        else
        {
            emission.rateOverTime = value;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
