using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Ability")]
public class MusicAbility : AbilityBase
{
    public GameObject skillNote;
    public GameObject[] noteSpawners;
    [HideInInspector] public bool spawningReady;

    public override void Activate(GameObject parent)
    {
        AbilityManager.instance.StartCoroutine(noteSpawn(parent));
    }

    public IEnumerator noteSpawn(GameObject parent)
    {
        spawningReady = false;

        if(skillNote.tag == "Note2")//green note
        {
            //turn on ability 2 particle system
            parent.GetComponent<PlayerParticleSystem>().PlayParticleSystem(1);
        }

        if(!AbilityManager.instance.usingSpawner) //if first spawner is not being used
        {
            AbilityManager.instance.usingSpawner = true; //using first spawner
            for (int i = 0; i < Random.Range(3,6); i++)
            {
                GameObject note = Instantiate(skillNote, noteSpawners[0].transform.position, Quaternion.identity); //instantiate at first spawner
                note.GetComponent<NoteScript>().player = parent;
                yield return new WaitForSeconds(1f);
            }
        }
        else if(AbilityManager.instance.usingSpawner) //first spawner being used
        {
            for (int i = 0; i < Random.Range(3,6); i++)
            {
                GameObject note = Instantiate(skillNote, noteSpawners[1].transform.position, Quaternion.identity); //instantiate at second spawner
                note.GetComponent<NoteScript>().player = parent;
                yield return new WaitForSeconds(1f);
            }
        }

        spawningReady = true;
    }
}
