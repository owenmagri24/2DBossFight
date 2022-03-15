using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Ability")]
public class MusicAbility : AbilityBase
{
    public GameObject skillNote;
    public GameObject noteSpawner;
    [HideInInspector] public bool spawningReady;

    public override void Activate(GameObject parent)
    {
        PlayerMovement.instance.StartCoroutine(noteSpawn()); //using playermovement instance because scriptableobjects cannot start coroutines
    }

    public IEnumerator noteSpawn()
    {
        spawningReady = false;

        for (int i = 0; i < Random.Range(3,6); i++)
        {
            GameObject note = Instantiate(skillNote, noteSpawner.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        spawningReady = true;
    }
}
