using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BarrageParticle : MonoBehaviour
{
    private PhotonView photonView;
    private ParticleSystem ps;
    [SerializeField] private float particleDamage = 0.4f;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    private int currentNumberOfParticles = 0;
    
    void Awake()
    {
        photonView = GetComponentInParent<PhotonView>();
        ps = GetComponent<ParticleSystem>();
    }

    private void Update() {

        if(ps.particleCount > currentNumberOfParticles)
        {
            //play sound
            SoundManager.instance.PlaySound("BarrageShoot");
        }

        currentNumberOfParticles = ps.particleCount;
    }

    private void OnParticleCollision(GameObject other) {
        if(!photonView.IsMine) { return; }

        int events = ps.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.tag == "Boss")
            {
                PhotonView target = other.gameObject.GetComponent<PhotonView>();
                target.RPC("ReduceHealth", RpcTarget.All, particleDamage);
                CinemachineShake.instance.ShakeCamera(0.7f, 0.2f);
            }
        }
    }
}
