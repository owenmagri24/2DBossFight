using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float noteSpeed;
    private Rigidbody2D rb;
    private Animator anim;

    public GameObject player;
    private ParticleSystem playerCirclePS;
    
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        AbilityManager.instance.notesList.Add(this);
    }


    private void FixedUpdate() {
        rb.velocity = Vector2.left * noteSpeed;

        if(gameObject.transform.position.x < -10f) //to change
        {
            AbilityManager.instance.notesList.Remove(this);
            Destroy(this.gameObject);
        }
    }
    
    public void PlayAbilityPs()
    {
        if(player == null){ return; }
        
        if(gameObject.tag == "Note") //Red Note
        {
            //Play red note ability with every note hit
            player.TryGetComponent<PlayerParticleSystem>(out var playerParticleSystem);
            playerParticleSystem.PlayParticleSystem(0);
            SoundManager.instance.PlaySound("QHit");
        }
        else if(gameObject.tag == "Note2") //Green note
        {
            //play green note ability
            PlayerParticleSystem playerPSScript = player.GetComponent<PlayerParticleSystem>();
            //increase emissionrate with every note hit
            float emissionRate = playerPSScript.GetPlayerEmissionRate(1);
            playerPSScript.ChangePlayerEmissionRate(1, emissionRate + 0.6f);
            SoundManager.instance.PlaySound("EHit");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerSkillCheck")
        {
            other.GetComponent<SkillCheck>().inSkillCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "PlayerSkillCheck")
        {
            other.GetComponent<SkillCheck>().inSkillCheck = false;
        }
    }

    public void NoteDestroyAnimation()
    {
        anim.SetTrigger("DestroyAnim");
    }

    private void DestroyNote()
    {
        Destroy(gameObject);
    }

}
