using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIState : State 
{
    [SerializeField] State damageState;
    
    protected Enemy enemy;

    protected override void AfterInit()
    {
        enemy = rootObject.GetComponent<Enemy>();
        enemy.Health.OnDamage += Damage;
        AfterEnemyAIInit();
    }

    protected virtual void AfterEnemyAIInit()
    {
        
    }

    public void Damage() {
        stateMachine.TransitionTo(damageState);
    }

    private void OnDestroy() {
        if(enemy != null)
            enemy.Health.OnDamage -= Damage;
    }
}
