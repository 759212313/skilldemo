using UnityEngine;
using System.Collections;

public class AIChaseState : AIBaseState
{
    public override void Enter()
    {

    }

    public override void Execute()
    {
        switch (Owner.ActorType)
        {
            case EActorType.MONSTER:
                {
                    if (Owner.GetTarget() == null)
                    {
                        return;
                    }
                    float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);
                    if (dist > AI.WARDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_BACK);
                    }
                    else if(dist<AI.ATKDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_FIGHT);
                        return;
                    }
                }
                break;
            case EActorType.PARTNER:
                {
                    if (Owner.GetTarget() == null)
                    {
                        AI.ChangeAIState(EAIState.AI_IDLE);
                        return;
                    }
                    float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);
                    if (dist > AI.WARDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_IDLE);
                        return;
                    }
                    else if (dist < AI.ATKDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_FIGHT);
                        return;
                    }
                }
                break;
            case EActorType.PLAYER:
                {
                    if (Owner.GetTarget() == null)
                    {
                        AI.ChangeAIState(EAIState.AI_IDLE);
                        return;
                    }
                    float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);

                    if (dist < AI.ATKDIST)
                    {
                        AI.ChangeAIState(EAIState.AI_FIGHT);
                        return;
                    }
                }
                break;
        }
        if (Owner.GetTarget() != null)
        {
            Owner.Command(new RTCommand(Owner.GetTarget()));
        }

    }

    public override void Exit()
    {

    }
}
