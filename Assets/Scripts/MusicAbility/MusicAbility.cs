using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Ability")]
public class MusicAbility : AbilityBase
{
    public GameObject skillNote;
    //public GameObject noteSpawner;
    public GameObject[] noteSpawners;
    [HideInInspector] public bool spawningReady;
    public bool usingSpawner = false; //put these in abilitymanager

    public override void Activate(GameObject parent)
    {
        PlayerMovement.instance.StartCoroutine(note1Spawn()); //using playermovement instance because scriptableobjects cannot start coroutines
    }

    public IEnumerator note1Spawn()
    {
        spawningReady = false;

        if(!usingSpawner)
        {
            usingSpawner = true;
            for (int i = 0; i < Random.Range(3,6); i++)
            {
                Instantiate(skillNote, noteSpawners[0].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
            usingSpawner = false;
        }
        else if(usingSpawner)
        {
            for (int i = 0; i < Random.Range(3,6); i++)
            {
                Instantiate(skillNote, noteSpawners[1].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
            usingSpawner = false;
        }

        spawningReady = true;
    }
}
