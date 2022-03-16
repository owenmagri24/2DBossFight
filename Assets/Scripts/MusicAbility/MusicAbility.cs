using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Ability")]
public class MusicAbility : AbilityBase
{
    public GameObject skillNote;
    public GameObject[] noteSpawners;
    [HideInInspector] public bool spawningReady;
    [HideInInspector] public GameObject note;

    public override void Activate(GameObject parent)
    {
        AbilityManager.instance.StartCoroutine(noteSpawn());
    }

    public IEnumerator noteSpawn()
    {
        spawningReady = false;

        if(!AbilityManager.instance.usingSpawner) //if first spawner is not being used
        {
            AbilityManager.instance.usingSpawner = true; //using first spawner
            for (int i = 0; i < Random.Range(3,6); i++)
            {
                note = Instantiate(skillNote, noteSpawners[0].transform.position, Quaternion.identity); //instantiate at first spawner
                yield return new WaitForSeconds(1f);
            }
            AbilityManager.instance.usingSpawner = false; //finished using first spawner
        }
        else if(AbilityManager.instance.usingSpawner) //first spawner being used
        {
            for (int i = 0; i < Random.Range(3,6); i++)
            {
                note = Instantiate(skillNote, noteSpawners[1].transform.position, Quaternion.identity); //instantiate at second spawner
                yield return new WaitForSeconds(1f);
            }
            AbilityManager.instance.usingSpawner = false; //finished usingspawner
        }

        spawningReady = true;
    }
}
