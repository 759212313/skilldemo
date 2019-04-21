using UnityEngine;
using System.Collections;

public class AIFightState : AIBaseState
{
    public override void Enter()
    {

    }

    public override void Execute()
    {
        Actor pTarget = Owner.GetTarget();
        switch (Owner.ActorType)
        {
            case EActorType.MONSTER:
                {
                    if (pTarget == null)
                    {
                        AI.ChangeAIState(EAIState.AI_BACK);
                        return;
                    }
                    Fight();
                }
                break;
            case EActorType.PARTNER:
                {
                    if (pTarget == null)
                    {
                        AI.ChangeAIState(EAIState.AI_FOLLOW);
                        return;
                    }
                    Fight();
                }
                break;
            case EActorType.PLAYER:
                {
                    if (pTarget == null)
                    {
                        AI.ChangeAIState(EAIState.AI_IDLE);
                        return;
                    }
                    Fight();
                }
                break;
        }


    }

    private void Fight()
    {
        float dist = GTTools.GetHorizontalDistance(Owner.Pos, Owner.GetTarget().Pos);
        if (dist > AI.ATKDIST)
        {
            AI.ChangeAIState(EAIState.AI_CHASE);
            return;
        }
        if(Owner.FSM==FSMState.FSM_SKILL)
        {
            return;
        }
        SkillTree skill = Owner.GetActorSkill().FindNextSkillByDist(dist);
        if (skill != null)
        {
            Owner.Command(new USCommand(skill.Pos));
        }
        else
        {
            Owner.Command(new IDCommand());
        }
    }

    public override void Exit()
    {

    }
}
