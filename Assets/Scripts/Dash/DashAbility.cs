using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dash Ability")]
public class DashAbility : AbilityBase
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        rb.velocity = movement.movementInput.normalized * dashVelocity;
    }
}
