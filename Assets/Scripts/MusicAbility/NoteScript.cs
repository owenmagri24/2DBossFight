using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float noteSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Start() {
    }

    private void FixedUpdate() {
        rb.velocity = Vector2.left * noteSpeed;

        if(gameObject.transform.position.x < -10f) //to change
        {
            Destroy(this.gameObject);
        }
    }
    
    public void PlayAbilityPs()
    {
        if(gameObject.tag == "Note") //Red Note
        {
            //Play red note ability with every note hit
            ParticleSystemManager.instance.playerParticleSystems[0].Play();
        }
        else if(gameObject.tag == "Note2")
        {
            //play green note ability
            //increase emissionrate with every note hit
            float emissionRate = ParticleSystemManager.instance.GetEmissionRate(ParticleSystemManager.instance.playerParticleSystems[1]);
            ParticleSystemManager.instance.ChangeEmissionRate(ParticleSystemManager.instance.playerParticleSystems[1], emissionRate + 0.6f);
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
