using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MusicAbilityParticle : MonoBehaviour
{
    private PhotonView photonView;
    new private ParticleSystem particleSystem;
    [SerializeField] private float particleDamage = 4f;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    void Awake()
    {
        photonView = GetComponentInParent<PhotonView>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        if(!photonView.IsMine) { return; }

        int events = particleSystem.GetCollisionEvents(other, colEvents);

        for (int i = 0; i < events; i++)
        {
            if(other.tag == "Boss")
            {
                PhotonView target = other.gameObject.GetComponent<PhotonView>();
                target.RPC("ReduceHealth", RpcTarget.All, particleDamage);
            }
        }
    }
}
