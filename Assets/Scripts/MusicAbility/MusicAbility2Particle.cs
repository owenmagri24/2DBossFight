using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbility2Particle : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle [] particles;
    [SerializeField] private float particleDamage = 0.5f;
    private Transform bossPos;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        bossPos = GameObject.FindObjectOfType<BossController>().transform;
    }

    void Update()
    {
        particles = new ParticleSystem.Particle [ps.particleCount];
        ps.GetParticles (particles);
        
    }

    public void ShootMusicAbility2Particles()
    {
        var main = ps.main;
        main.simulationSpace = ParticleSystemSimulationSpace.World; //change simulation to world

        for (int i = 0; i < particles.GetUpperBound (0); i++) {
            particles[i].position = transform.parent.position; //set particle position to player pos
            float ForceToAdd = (2 *Vector3.Distance (bossPos.position, particles[i].position));
            particles[i].velocity = (bossPos.position - particles[i].position).normalized * ForceToAdd;
        }
        ps.SetParticles(particles, particles.Length);
        
        ps.Stop();
        
        gameObject.GetComponentInParent<PlayerParticleSystem>().ChangePlayerEmissionRate(1, 1f); //rotatingPS
    }

    private void OnParticleCollision(GameObject other) {
        int events = ps.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<BossController>(out BossController bossController))
            {
                bossController.health -= particleDamage;
                CinemachineShake.instance.ShakeCamera(2f, 0.3f);
            }
        }
    }
}
