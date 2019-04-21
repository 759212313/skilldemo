using UnityEngine;
using System.Collections;

public class AIGlobalState :AIBaseState
{

    public override void Enter()
    {

    }

    public override void Execute()
    {
        if(Owner.IsDead())
        {
            AI.ChangeAIState(EAIState.AI_DEAD);
            return;
        }
        if (AI.AIMode == EAIMode.Auto && Owner.GetTarget() == null)
        {
            IntervalFindEnemy();
        }
    }

    

    public override void Exit()
    {

    }

    private void IntervalFindEnemy()
    {
        if (AI.FindEnemyTimer >= Define.MIN_INTERVAL_FINDENEMY)
        {
            Actor enemy = Owner.GetNearestEnemy(AI.WARDIST);
            if (enemy != null)
            {
                Debug.LogError(enemy.Id);
            }
            this.Owner.SetTarget(enemy);
            AI.FindEnemyTimer = 0;
        }
        else
        {
            AI.FindEnemyTimer += Time.deltaTime;
        }
    }
}
