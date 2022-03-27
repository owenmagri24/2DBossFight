using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager instance;

    public ParticleSystem[] bossParticleSystems;
    public ParticleSystem[] playerParticleSystems;

    private void Awake() {
        if(instance == null)
            ParticleSystemManager.instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    public ParticleSystem GetRandomBossParticleSystem()
    {
        int rand = Random.Range(0, bossParticleSystems.Length);
        return bossParticleSystems[rand];
    }

    public void ChangeParticleSpeed(ParticleSystem ps, float value)
    {
        var main = ps.main;
        main.simulationSpeed = value;
    }

    public void ResetParticleSpeed(ParticleSystem ps,float value)
    {
        var main = ps.main;
        main.simulationSpeed = value;
    }

    public void ChangeEmissionRate(ParticleSystem ps, float value)
    {
        var emission = ps.emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            emission.SetBurst(0,
            new ParticleSystem.Burst(0, value)); //set burst to value
        }
        else
        {
            emission.rateOverTime = value;
        }
    }

    public float GetEmissionRate(ParticleSystem ps)
    {
        var emission = ps.emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            var burst = emission.GetBurst(0).count;
            return burst.constant;
        }
        else
            return emission.rateOverTime.constant;
    }

    public void PlayerMusicAbility2()
    {
        var main = playerParticleSystems[1].main;
        if(main.simulationSpace == ParticleSystemSimulationSpace.World)
        {
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
        }
        playerParticleSystems[1].Play();
    } 
}
