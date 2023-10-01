using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTargetByTagAndDistanceAction : SearchTargetAction
{
    [Space]
    [SerializeField] protected List<string> withTag;
    [SerializeField] float minDistance = -1f;
    [SerializeField] float maxDistance = -1f;
    [SerializeField] bool findTheClosest = true;   

    protected override IEnumerator ExecuteCO()
    {
        Targets = new List<Transform>();
        Target = null;

        List<GameObject> possibleTargets = new List<GameObject>();
        foreach (var tagName in withTag)
        {
            possibleTargets.AddRange(GameObject.FindGameObjectsWithTag(tagName));
        }

        if (possibleTargets == null || possibleTargets.Count == 0)
            yield break;

        float distance = findTheClosest ? float.PositiveInfinity : 0;

        foreach (var possibleTarget in possibleTargets)
        {
            float d = Vector3.Distance(transform.position, possibleTarget.transform.position);

            if (maxDistance > 0 && d >= maxDistance)
            {
                continue;
            }

            if (minDistance > 0 && d <= minDistance)
            {
                continue;
            }

            Targets.Add(possibleTarget.transform);

            if (findTheClosest == true && d < distance)
            {
                Target = possibleTarget.transform;
                distance = d;
                continue;
            }

            if (findTheClosest == false && d > distance)
            {
                Target = possibleTarget.transform;
                distance = d;
                continue;
            }
        }
    }
}
