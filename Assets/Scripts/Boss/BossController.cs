using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossController : MonoBehaviour
{
    //Movement and Direction
    public float moveSpeed;
    private bool moveRight;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    //Health
    [SerializeField] private float startingHealth;
    [HideInInspector] public float health;

    //Photon
    private PhotonView photonView;

    //ParticleSystem
    [SerializeField] private ParticleSystem[] particleSystems;


    private void Awake() {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponentInChildren<Animator>();
        photonView = GetComponent<PhotonView>();
    }
    
    private void Start() {
        moveRight = true;
        spriteRenderer.flipX = false;
        startingHealth = startingHealth * PhotonNetwork.PlayerList.Length; //boss hp scales with num of players
        health = startingHealth; 
    }

    private void Update() {
        if(!PhotonNetwork.IsMasterClient) { return; }
        
        if(health <= startingHealth/2) //half hp
        {
            //anim.SetTrigger("Phase2");
            ChangeAnimation("Phase2");
        }
        if(health <= 0)
        {
            //anim.SetTrigger("Death");
            ChangeAnimation("Death");
        }
    }

    public void ChangeAnimation(string name)
    {
        photonView.RPC("RPC_ChangeAnimation", RpcTarget.All, name);
    }

    [PunRPC]
    void RPC_ChangeAnimation(string name)
    {
        anim.SetTrigger(name);
    }
    
    public void BossMove()
    {
        if(transform.position.x > 7f)
        {
            photonView.RPC("RPC_MoveLeft", RpcTarget.All, true);
        }
        else if(transform.position.x < -7f)
        {
            photonView.RPC("RPC_MoveLeft", RpcTarget.All, false);
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

    [PunRPC]
    void RPC_MoveLeft(bool left)
    {
        if(left == true)
        {
            moveRight = false;
            spriteRenderer.flipX = true;
        }
        else
        {
            moveRight = true;
            spriteRenderer.flipX = false;
        }
    }
    
    public void Death()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        
        PhotonNetwork.CleanRpcBufferIfMine(photonView);
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC] public void ReduceHealth(float value)
    {
        if(photonView.IsMine)
        {
            SoundManager.instance.PlaySound("BossHit");
            health -= value;
        }
        else
        {
            health -= value;
        }
        Debug.Log(health);
    }

    //----- Boss Particle System -----

    public ParticleSystem GetRandomBossPs()
    {
        int rand = Random.Range(0, particleSystems.Length);
        return particleSystems[rand];
    }

    public int ReturnWhichParticleSystem(ParticleSystem ps) //Returns the parameter ps as int from bossParticleSystem[]
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            if(ps.name == particleSystems[i].name)
            {
                return i;
            }
        }
        return 0;
    }

    public void PlayParticleSystem(int whichPS)
    {
        photonView.RPC("RPC_PlayParticleSystem", RpcTarget.All, whichPS);
    }

    [PunRPC]
    void RPC_PlayParticleSystem(int whichPS)
    {
        particleSystems[whichPS].Play();
    }

    public void StopParticleSystem(int whichPS)
    {
        photonView.RPC("RPC_StopPS", RpcTarget.All, whichPS);
    }

    [PunRPC]
    void RPC_StopPS(int whichPS)
    {
        particleSystems[whichPS].Stop();
    }

    public void ChangeParticleSpeed(int whichPS, float value)
    {
        photonView.RPC("RPC_ChangeParticleSpeed", RpcTarget.All, whichPS, value);
    }

    [PunRPC]
    void RPC_ChangeParticleSpeed(int whichPS, float value)
    {
        var main = particleSystems[whichPS].main;
        main.simulationSpeed = value;
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

    public void ChangeEmissionRate(int whichPS, float value)
    {
        photonView.RPC("RPC_ChangeEmissionRate", RpcTarget.All, whichPS, value);
    }

    [PunRPC]
    void RPC_ChangeEmissionRate(int whichPS, float value)
    {
        var emission = particleSystems[whichPS].emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            emission.SetBurst(0,
            new ParticleSystem.Burst(0, value)); //set burst to value
        }
        else
        {
            emission.rateOverTime = value;
        }
    }
}
