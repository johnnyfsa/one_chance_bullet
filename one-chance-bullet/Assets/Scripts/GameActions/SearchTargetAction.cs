using System.Collections.Generic;
using UnityEngine;

public abstract class SearchTargetAction : GameAction
{
    protected Transform target;
    public Transform Target { get => target; protected set => target = value; }

    protected List<Transform> targets;
    public List<Transform> Targets { get => targets; protected set => targets = value; }
}
