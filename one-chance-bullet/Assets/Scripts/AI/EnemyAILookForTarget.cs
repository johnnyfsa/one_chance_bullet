using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAILookForTarget : EnemyAIState
{
    [Space]
    [SerializeField] float lookRadius = 100f;
    [SerializeField] float lookForTargetInterval = 0.5f;    
    
    [Space]
    [SerializeField] State foundTargetState;

   protected override IEnumerator StateCO()
    {
        while(true)
        {
            Player player = GetClosestPlayerAlive(enemy.transform.position);
            if(player != null)
            {
                enemy.SetTarget(player);
                stateMachine.TransitionTo(foundTargetState);
                yield break;
            }
            yield return new WaitForSeconds(lookForTargetInterval);
        }
    }

    Player GetClosestPlayerAlive(Vector2 enemyPosition)
    {
        Player[] players = FindObjectsOfType<Player>();
        players = players.Where(p => !p.IsDead).ToArray();
        players = players.Where(p => Vector2.Distance(enemyPosition, p.transform.position) <= lookRadius).ToArray();
        return players.OrderBy(p => Vector2.Distance(enemyPosition, p.transform.position)).FirstOrDefault();        
    }
}
