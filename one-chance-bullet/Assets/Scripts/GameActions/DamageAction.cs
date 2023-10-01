using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DamageAction : GameAction<GameObject>
{
    [SerializeField] float damageValue;

    protected override IEnumerator ExecuteCO()
    {
        if (!actOn.TryGetComponent<Health>(out var targetHealth))
            yield break;

        targetHealth.TakeDamage(damageValue);
    }
}
