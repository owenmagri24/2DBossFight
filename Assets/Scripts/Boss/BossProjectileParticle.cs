using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileParticle : MonoBehaviour
{
    [SerializeField] new private ParticleSystem particleSystem;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other) {
        int events = particleSystem.GetCollisionEvents(other, colEvents);

        
        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                //Hit Player
            }
        }
    }
}
