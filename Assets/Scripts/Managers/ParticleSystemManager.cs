using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParticleSystemManager : MonoBehaviour
{
    public static ParticleSystemManager instance;
    public ParticleSystem[] bossParticleSystems;
    [SerializeField] private GameObject bossPrefab;
    private PhotonView photonView;

    private void Awake() {
        if(instance == null)
            ParticleSystemManager.instance = this;
        else if(instance != this)
            Destroy(gameObject);

        photonView = GetComponent<PhotonView>();
    }


    public ParticleSystem GetRandomBossParticleSystem()
    {
        int rand = Random.Range(0, bossParticleSystems.Length);
        return bossParticleSystems[rand];
    }

    public int ReturnWhichParticleSystem(ParticleSystem ps) //Returns the parameter ps as int from bossParticleSystem[]
    {
        for (int i = 0; i < bossParticleSystems.Length; i++)
        {
            if(ps.name == bossParticleSystems[i].name)
            {
                return i;
            }
        }
        return 0;
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

    public void PlayParticleSystem(int whichPS)
    {
        photonView.RPC("RPC_PlayParticleSystem", RpcTarget.All, whichPS);
    }

    [PunRPC]
    void RPC_PlayParticleSystem(int whichPS)
    {
        bossParticleSystems[whichPS].Play();
    }
}
