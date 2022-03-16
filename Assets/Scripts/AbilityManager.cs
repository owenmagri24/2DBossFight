using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public bool usingSpawner;
    //public int whichPresser;

    void Awake()
    {
        if(instance == null)
            AbilityManager.instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

}
