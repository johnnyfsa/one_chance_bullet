using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceAction : GameActionComponent<Rigidbody2D>
{
    [SerializeField] Vector2 direction;
    [SerializeField] float force;

    protected override IEnumerator ExecuteCO()
    {
        component.velocity = Vector2.zero;
        Vector2 dir = transform.rotation * Vector3.up;
        component.AddForce(dir * force, ForceMode2D.Impulse);

        yield break;
    }
}
