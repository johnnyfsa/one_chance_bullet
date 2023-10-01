using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIAttackState : EnemyAIState
{
    [Space]
    [SerializeField] State finishAttackState;

    protected override void AfterEnemyAIInit()
    {
        enemy.OnAttackEnd += EndAttack;
    }

    public void EndAttack() {
        stateMachine.TransitionTo(finishAttackState);
    }

    private void OnDestroy() {
        if(enemy != null)
            enemy.OnAttackEnd -= EndAttack;
    }

}
