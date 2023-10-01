using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectAction : GameAction
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform creationPoint;
    [SerializeField] float loopTime = -1;

    private void Start()
    {
        if (creationPoint == null)
            creationPoint = transform;
    }

    protected override IEnumerator ExecuteCO()
    {
        do
        {
            Pool.CreateObject(prefab, creationPoint.position, creationPoint.rotation);
            yield return new WaitForSeconds(loopTime);
        } while (loopTime > 0);
        
    }
}
