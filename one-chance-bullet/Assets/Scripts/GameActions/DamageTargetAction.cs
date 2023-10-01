using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTargetAction : GameAction
{
    [Space]
    [SerializeField] float damageValue;
    [Space]
    [SerializeField] float loopTime = 0;
    [SerializeField] int loopNumber = 1;
    [Space]
    [SerializeField] bool searchWhenExecute;
    [SerializeField] SearchTargetAction searchTarget;
    [SerializeField] bool damageAll = false;

    protected override IEnumerator ExecuteCO()
    {
        loopNumber = this.loopTime <= 0 ? 1 : loopNumber;

        if (searchWhenExecute == true)
            searchTarget.Execute();

        if(damageAll == true)
        {
            foreach(var target in searchTarget.Targets)
            {
                StartCoroutine(DamageHealthCO(target));
            }
        } else
        {
            StartCoroutine(DamageHealthCO(searchTarget.Target));
        }

        yield break;   
    }

    protected IEnumerator DamageHealthCO(Transform target)
    {
        if (!target.TryGetComponent<Health>(out var targetHealth))
            yield break;

        for (int i = 0; i < loopNumber; i++)
        {
            targetHealth.TakeDamage(damageValue);

            if (loopTime > 0)
                yield return new WaitForSeconds(loopTime);
            else
                break;
        }
    }
}
