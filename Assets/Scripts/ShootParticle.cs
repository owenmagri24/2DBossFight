using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticle : MonoBehaviour
{
    [SerializeField] new private ParticleSystem particleSystem;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    private float canFireTime;
    [SerializeField] private float startCanFireTime;

    private void Update() 
    {
        if(canFireTime <= 0) // if can fire
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                particleSystem.Play(); //shoot particle
                canFireTime = startCanFireTime; //Reset CanFire timer
            }
        }
        else
        {
            canFireTime -= Time.deltaTime; //reduce canfiretime per second
        }
    }

    private void OnParticleCollision(GameObject other) {
        int events = particleSystem.GetCollisionEvents(other, colEvents);

        
        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<BossController>(out BossController bossController))
            {
                bossController.health -= 2f;
            }
        }
    }
}
