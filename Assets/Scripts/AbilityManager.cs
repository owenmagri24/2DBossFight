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

    public void TurnOffPresser(GameObject presser, float delay)
    {
        StartCoroutine(TurnPresserOff(presser, delay));
    }

    IEnumerator TurnPresserOff(GameObject _presser, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _presser.SetActive(false);
        if(_presser == playerPressers[0])
        {
            usingSpawner = false;
        }
    }

}
