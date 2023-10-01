using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIChaseTargetState : EnemyAIState
{
    [Space]
    [SerializeField] float attackRadius = 1.5f;
    [SerializeField] float runAwayRadius = 0;
    [SerializeField] float changeDirectionInterval = 0.1f;

    [Space]
    [SerializeField] State closeToAttackState;
    
    CharacterMovement2D characterMovment;

    protected override void AfterEnemyAIInit()
    {
        characterMovment = rootObject.GetComponent<CharacterMovement2D>();
    }

    protected override void AfterExit()
    {
        characterMovment.MovementDirection = Vector2.zero;
    }

    protected override IEnumerator StateCO()
    {
        while(true)
        {
            var distance = Vector2.Distance(enemy.transform.position, enemy.TargetPlayer.transform.position);
            if(distance <= attackRadius && distance > runAwayRadius)
            {
                stateMachine.TransitionTo(closeToAttackState);
                yield break;
            }

            if(distance <= runAwayRadius)
            {
                characterMovment.MovementDirection = enemy.transform.position - enemy.TargetPlayer.transform.position;                
            } else {
                characterMovment.MovementDirection = enemy.TargetPlayer.transform.position - enemy.transform.position;
            }

            yield return new WaitForSeconds(changeDirectionInterval);
        }

        
    }
}
