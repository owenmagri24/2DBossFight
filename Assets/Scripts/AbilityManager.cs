using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public bool usingSpawner; //used in MusicAbility
    public GameObject[] playerPressers;

    void Awake()
    {
        if(instance == null)
            AbilityManager.instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    public GameObject checkWhichPresser() //returns which playerPresser to use
    {
        for (int i = 0; i < playerPressers.Length; i++)
        {
            if(playerPressers[i].activeSelf)
            {
                continue;
            }
            else if(!playerPressers[i].activeSelf)
            {
                return playerPressers[i];
            }
        }
        Debug.Log("return null");
        return null;
    }

}
