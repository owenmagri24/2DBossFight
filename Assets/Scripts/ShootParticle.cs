using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootParticle : MonoBehaviour
{
    private PhotonView photonView;
    private ParticleSystem ps;
    private PlayerMovement playerMovement;

    [SerializeField] private float particleDamage = 1.5f;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    
    private void Awake() 
    {
        photonView = GetComponentInParent<PhotonView>();
        ps = GetComponent<ParticleSystem>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(!photonView.IsMine) { return; }

        int events = ps.GetCollisionEvents(other, colEvents);
        
        for (int i = 0; i < events; i++)
        {
            if(other.tag == "Boss")
            {
                PhotonView target = other.gameObject.GetComponent<PhotonView>();
                target.RPC("ReduceHealth", RpcTarget.All, particleDamage);
                playerMovement.DealtDamage(particleDamage);
            }
        }
    }
}
