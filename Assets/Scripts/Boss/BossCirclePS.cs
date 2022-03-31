using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCirclePS : MonoBehaviour
{
    [SerializeField] private float particleDamage;
    [SerializeField] new private ParticleSystem particleSystem;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other) {
        int events = particleSystem.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                //Hit Player
                playerMovement.PlayerHit(particleDamage);
            }
        }
    }
}
