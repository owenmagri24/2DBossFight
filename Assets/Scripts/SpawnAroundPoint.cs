using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAroundPoint : MonoBehaviour
{
    public GameObject pivotObject;

    private void OnEnable() {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0,0,1), Random.Range(0, 360));
    }
}
