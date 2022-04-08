using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CinemachineTargetGroup cm_TargetGroup;

    private void Awake() 
    {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomNumber];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        if(playerToSpawn == null)
        {
            playerToSpawn = playerPrefabs[0];
        }
        GameObject playerSpawned = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        cm_TargetGroup.AddMember(playerSpawned.transform, 1, 0);
    }
}
