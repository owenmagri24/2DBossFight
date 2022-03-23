using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotgunPS : MonoBehaviour
{
    float angle = 90f;
    float timer;
    private ParticleSystem ps;

    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        timer = ps.main.duration;
        InvokeRepeating("RotatePS", 0.3f, timer);
    }



    void RotatePS()
    {
        transform.Rotate(0,0, angle);
    }
}
