using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField usernameInput;
    [SerializeField] private Text buttonText;

    public void OnClickConnect() //When player clicks connect button
    {
        if(usernameInput.text.Length >= 1) //username not empty
        {
            PhotonNetwork.NickName = usernameInput.text; //assign username to his photon nickname
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true; //loads same level as master client
            PhotonNetwork.ConnectUsingSettings(); // connect to server
        }
    }

    public override void OnConnectedToMaster() //When player connects to server
    {
        SceneManager.LoadScene("Lobby");
    }
}
