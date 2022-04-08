using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootParticle : MonoBehaviour
{
    private PhotonView photonView;
    private ParticleSystem ps;

    [SerializeField] private float particleDamage = 1.5f;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    
    private void Awake() 
    {
        photonView = GetComponentInParent<PhotonView>();
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(!photonView.IsMine) { return; } //Only deal damage if its my particle

        int events = ps.GetCollisionEvents(other, colEvents);

        
        for (int i = 0; i < events; i++)
        {
            if(other.TryGetComponent<BossController>(out BossController bossController))
            {
                bossController.health -= particleDamage;
            }
        }
    }
}
