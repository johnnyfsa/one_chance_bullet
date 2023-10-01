using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : Pickup
{
    [SerializeField] float healAmount = 1.0f;

    protected override void OnPickup(Collider2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}
