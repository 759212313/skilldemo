using UnityEngine;
using System.Collections;
using System;

public class AIBaseState : IState<Actor,EAIState>
{
    public ActorAI AI
    {
        get
        {
            return Owner.GetActorAI();
        }
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {

    }

    public override void Exit()
    {

    }
}
