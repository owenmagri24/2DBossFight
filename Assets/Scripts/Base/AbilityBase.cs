using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;
    public KeyCode key;

    public virtual void Activate(GameObject parent) {}
}
