using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAbility2Particle : MonoBehaviour
{
    private ParticleSystem ps;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem.Particle [] particles;
    [SerializeField] private Transform bossPos;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
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
        ParticleSystemManager.instance.ChangeEmissionRate(ps, 1f); //reset emission rate to 1f
    }

    private void OnParticleCollision(GameObject other) {
        int events = ps.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<BossController>(out BossController bossController))
            {
                bossController.health -= 0.5f;
            }
        }
    }
}
