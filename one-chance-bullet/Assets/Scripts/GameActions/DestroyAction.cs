using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyAction : GameActionComponent<Transform>
{
    [Space]
    [SerializeField] bool deactivateInstedOfDestroy;
    [SerializeField] GameObject createObjectOnDestroy;

    protected override IEnumerator ExecuteCO()
    {
        if (!this.isActiveAndEnabled)
            yield break;

        if (createObjectOnDestroy != null)
            Pool.CreateObject(createObjectOnDestroy, transform.position, transform.rotation);

        if (deactivateInstedOfDestroy)
        {
            component.gameObject.SetActive(false);
        }
        else
        {
            Destroy(component.gameObject);
        }
    }
}
