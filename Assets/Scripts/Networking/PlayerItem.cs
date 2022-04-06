using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Text playerName;
    
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject leftArrowButton;
    [SerializeField] private GameObject rightArrowButton;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.SetActive(true);
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }
}
