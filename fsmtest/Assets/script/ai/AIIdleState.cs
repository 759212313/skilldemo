using UnityEngine;
using System.Collections;

public class AIIdleState : AIBaseState
{
    public override void Enter()
    {

    }

    public override void Execute()
    {
        Actor pTarget = Owner.GetTarget();
        if (pTarget != null)
        {
            Debug.LogError("找到的目標" + pTarget.Id);
        }
        
        switch (Owner.ActorType)
        {
            case EActorType.MONSTER:
                {
                    if (pTarget != null)
                    {
                        float dist = GTTools.GetHorizontalDistance(Owner.Pos, pTarget.Pos);
                        if (dist < AI.WARDIST)
                        {
                            AI.ChangeAIState(EAIState.AI_CHASE);
                        }
                    }
                }
                break;
            case EActorType.PARTNER:
                {
                    //if (pTarget!=null)
                    //{
                    //    float dist = GTTools.GetHorizontalDistance(Owner.Pos, pTarget.Pos);
                    //    if (dist < AI.WARDIST)
                    //    {
                    //        AI.ChangeAIState(EAIState.AI_CHASE);
                    //    }
                    //}
                    //Actor pHost = Owner.GetHost();
                    //if (pHost != null)
                    //{
                    //    float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetHost().Pos);
                    //    if (dist > AI.FOLLOWDIST)
                    //    {
                    //        AI.ChangeAIState(EAIState.AI_FOLLOW);
                    //        return;
                    //    }
                    //}
                }
                break;
            case EActorType.PLAYER:
                {
                    if (pTarget != null)
                    {
                        float dist = GTTools.GetHorizontalDistance(Owner.Pos, pTarget.Pos);
                        if (dist < AI.WARDIST)
                        {
                            AI.ChangeAIState(EAIState.AI_CHASE);
                        }
                    }
                }
                break;
        }
    }

    public override void Exit()
    {

    }
}
