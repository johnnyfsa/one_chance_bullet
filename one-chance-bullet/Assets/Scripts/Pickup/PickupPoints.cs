using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : Pickup
{
    [SerializeField] int pointsValue = 1;

    protected override void OnPickup(Collider2D collision)
    {
        var pointHandle = collision.gameObject.GetComponent<Points>();
        if (pointHandle != null)
        {
            pointHandle.Add(pointsValue);
        }
    }
}
