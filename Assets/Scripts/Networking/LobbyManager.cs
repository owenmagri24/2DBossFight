using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomInputField;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private Text roomName;
    [SerializeField] private Text regionName;

    [SerializeField] private RoomItem roomItemPrefab;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    [SerializeField] private Transform contentObject;

    private float timeBetweenUpdates = 1.5f;
    private float nextUpdateTime;

    [SerializeField] List<PlayerItem> playerItemsList = new List<PlayerItem>();
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent;

    [SerializeField] private GameObject playButton;

    private void Start() 
    {
        PhotonNetwork.JoinLobby(); // Joins a photon lobby when scene loads
        regionName.text = "Region: " + PhotonNetwork.CloudRegion;
    }

    private void Update() 
    {
        if(PhotonNetwork.IsMasterClient) //&& PhotonNetwork.CurrentRoom.PlayerCount >= 2) //if this player created the room and more than 1 player in room
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickCreate()
    {
        if(roomInputField.text.Length >= 1) //Room name not empty
        {
            //Creates room with desired name, set max players, and broadcast changes to all players
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){ MaxPlayers = 4, BroadcastPropsChangeToAll = true}); 
        }
    }

    public override void OnJoinedRoom() //Called when joining or creating a photon room
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //Gets called when there is a change in room list (Created, modified, destroyed)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> list) //When there is a change in photon room list
    {
        foreach (RoomItem item in roomItemsList) //destroy all rooms in list
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list) //Instantiate all roomItems available with correct names
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName); //This calls the OnJoinedRoom function above
    }

    public void OnClickLeaveRoom() //When clicks leave room button
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //Gets called when leaving room
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster() //When player leaves room, they rejoin lobby
    {
        PhotonNetwork.JoinLobby();
        regionName.text = "Region: " + PhotonNetwork.CloudRegion;
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if(PhotonNetwork.CurrentRoom == null) { return; }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players) // the dictionary of all players in a room
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if(player.Value == PhotonNetwork.LocalPlayer) //if this player is you
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //Called by Photon when new player enters room
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //Called by Photon when player leaves room
    {
        UpdatePlayerList();
    }

    public void OnClickPlayGame()
    {
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
