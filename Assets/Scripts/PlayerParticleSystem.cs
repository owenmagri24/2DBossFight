using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    private PhotonView photonView;

    private void Awake() {
        photonView = GetComponent<PhotonView>();
    }

    public void PlayParticleSystem(int whichPS)
    {
        if(whichPS == 1) //RotatingPS
        {
            var main = particleSystems[1].main; //RotatingPS
            if(main.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                main.simulationSpace = ParticleSystemSimulationSpace.Local;
            }
            //particleSystems[1].Play();
        }

        photonView.RPC("RPC_PlayParticleSystem", RpcTarget.All, whichPS);
    }

    [PunRPC]
    void RPC_PlayParticleSystem(int whichPS)
    {
        particleSystems[whichPS].Play();
    }

    
    public void ChangePlayerEmissionRate(int whichPS, float value)
    {
        photonView.RPC("RPC_ChangePlayerEmissionRate", RpcTarget.All, whichPS, value);
    }
    
    [PunRPC]
    void RPC_ChangePlayerEmissionRate(int whichPS, float value)
    {
        var emission = particleSystems[whichPS].emission;
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

    public float GetPlayerEmissionRate(int whichPS)
    {
        var emission = particleSystems[whichPS].emission;
        if(emission.rateOverTime.constant == 0) //if its a burst ps
        {
            var burst = emission.GetBurst(0).count;
            return burst.constant;
        }
        else
            return emission.rateOverTime.constant;
    }
}
