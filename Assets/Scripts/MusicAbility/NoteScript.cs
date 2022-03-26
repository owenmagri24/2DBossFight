using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    public float noteSpeed;
    Rigidbody2D rb;
    Animator anim;
    
    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
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
            AbilityManager.instance.particleSystems[0].Play();
        }
        else if(gameObject.tag == "Note2")
        {
            //play green note ability
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
