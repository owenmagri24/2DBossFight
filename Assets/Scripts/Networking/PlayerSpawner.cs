using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject bossPrefab;
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

    private void Update() {
        // if(PhotonNetwork.MasterClient == null)
        // {
        //     int rand = Random.Range(0, PhotonNetwork.PlayerList.Length);
        //     PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[rand]);
        //     Debug.Log("Changed Master to: "+ PhotonNetwork.MasterClient.NickName + " from " + PhotonNetwork.PlayerList.Length + " number of players.");
        // }
        Debug.Log(PhotonNetwork.MasterClient.NickName);
    }
}
