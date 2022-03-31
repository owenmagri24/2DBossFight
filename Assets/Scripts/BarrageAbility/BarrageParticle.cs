using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageParticle : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] private float particleDamage = 0.4f;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        int events = ps.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<BossController>(out BossController bossController))
            {
                bossController.health -= particleDamage;
                CinemachineShake.instance.ShakeCamera(0.7f, 0.2f);
            }
        }
    }
}
