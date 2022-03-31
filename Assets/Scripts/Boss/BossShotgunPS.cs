using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotgunPS : MonoBehaviour
{
    private float angle = 90f;
    private float timer;
    private ParticleSystem ps;
    [SerializeField] private float particleDamage;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        timer = ps.main.duration;
        InvokeRepeating("RotatePS", 0.3f, timer);
    }

    void RotatePS()
    {
        transform.Rotate(0,0, angle);
    }

    private void OnParticleCollision(GameObject other) {
        int events = ps.GetCollisionEvents(other, colEvents);

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
