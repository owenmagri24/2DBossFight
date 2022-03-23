using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float health;
    private bool moveRight;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem[] particleSystems;

    private void Awake() {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        
    }
    
    private void Start() {
        moveRight = true;
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
        emission.rateOverTime = value;
    }
}
