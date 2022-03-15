using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,-rotationSpeed) * Time.deltaTime);
    }
}
