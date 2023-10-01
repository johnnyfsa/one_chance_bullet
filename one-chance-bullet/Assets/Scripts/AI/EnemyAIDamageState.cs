using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIDamageState : EnemyAIState
{
    public State idleState;

    protected override void AfterEnemyAIInit()
    {
        enemy.OnDamageEnd += DamageEnd;
    }

    public void DamageEnd() {
        stateMachine.TransitionTo(idleState);
    }

    private void OnDestroy() {
        if(enemy != null)
            enemy.OnDamageEnd -= DamageEnd;
    }
}
