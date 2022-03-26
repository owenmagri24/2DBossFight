using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbilityParticle : MonoBehaviour
{
    new private ParticleSystem particleSystem;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Awake()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        int events = particleSystem.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<BossController>(out BossController bossController))
            {
                bossController.health -= 4f;
            }
        }
    }
}
