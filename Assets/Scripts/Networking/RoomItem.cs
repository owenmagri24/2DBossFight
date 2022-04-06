using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private Text roomName;
    [SerializeField] private LobbyManager lobbyManager;

    private void Start() 
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClickItem() //Called when player clicks room
    {
        lobbyManager.JoinRoom(roomName.text);
    }
}
