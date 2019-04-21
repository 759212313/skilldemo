using UnityEngine;
using System.Collections;

public class AIBackState : AIBaseState
{
    public override void Enter()
    {
        Owner.Command(new RTCommand(Owner.GetBornParam().Position, OnBackFinished));
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        Owner.CacheTransform.localPosition = Owner.GetBornParam().Position;
        Owner.CacheTransform.localEulerAngles = Owner.GetBornParam().EulerAngles;
    }

    private void OnBackFinished()
    {
        Owner.GetActorAI().ChangeAIState(EAIState.AI_IDLE);
    }
}
