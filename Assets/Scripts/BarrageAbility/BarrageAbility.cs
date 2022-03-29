using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Barrage Ability")]
public class BarrageAbility : AbilityBase
{

    public override void Activate(GameObject parent)
    {
        parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; //freeze player
    }
}
