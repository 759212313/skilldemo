using UnityEngine;
using System.Collections;
using System;

public class ActorAI : IGame
{
    public Actor Owner { get; private set; }
    public EAIState AIState { get; private set; }
    public EAIMode AIMode { get; set; }
    public IStateMachine<Actor,EAIState> AIStateMachine { get; private set; }

    public float WARDIST//搜索距离
    {
        get { return 30; }
    }

    public float ATKDIST//攻击距离
    {
        get { return 3; }// Owner.GetActorSkill().GetMinCastDistance(); }
    }

    public float FOLLOWDIST//跟随距离
    {
        get { return 4; }
    }

    public float FindEnemyTimer = Define.MIN_INTERVAL_FINDENEMY;

    public ActorAI(Actor pOwner)
    {
        this.Owner = pOwner;
        this.AIStateMachine = new IStateMachine<Actor,EAIState>(pOwner);
        this.AIState = EAIState.AI_IDLE;
        if(Owner is ActorMainPlayer)
        {
            this.AIMode = EAIMode.Hand;
        }
        else
        {
            Debug.Log("初始化为自动类型");
            this.AIMode = EAIMode.Auto;
        }
        this.AIStateMachine.AddState(EAIState.AI_IDLE, new AIIdleState());
        //this.AIStateMachine.AddState(EAIState.AI_FOLLOW, new AIFollowState());
        //this.AIStateMachine.AddState(EAIState.AI_FLEE, new AIFleeState());
        //this.AIStateMachine.AddState(EAIState.AI_PATROL, new AIPatrolState());
        //this.AIStateMachine.AddState(EAIState.AI_ESCAPE, new AIEscapeState());
        //this.AIStateMachine.AddState(EAIState.AI_BACK, new AIBackState());
        this.AIStateMachine.AddState(EAIState.AI_FIGHT, new AIFightState());
        //this.AIStateMachine.AddState(EAIState.AI_DEAD, new AIDeadState());
        this.AIStateMachine.AddState(EAIState.AI_CHASE, new AIChaseState());
        this.AIStateMachine.AddState(EAIState.AI_GLOBAL, new AIGlobalState());
    }


    public void Start()
    {
        if (AIMode == EAIMode.Hand || Owner.IsDead())
        {
            return;
        }
        IState<Actor, EAIState> initState = this.AIStateMachine.GetState(AIState);
        IState<Actor, EAIState> globalState = this.AIStateMachine.GetState(EAIState.AI_GLOBAL);
        this.AIStateMachine.SetCurrState(initState);
        this.AIStateMachine.SetGlobalState(globalState);
        AIStateMachine.Start();
    }

    public void Step()
    {
        if (AIMode == EAIMode.Hand || Owner.IsDead())
        {
            return;
        }
        AIStateMachine.Step();    
    }

    public void ChangeAIState(EAIState pAIState)
    {
        if (AIState != pAIState)
        {
            AIState = pAIState;
            AIStateMachine.ChangeState(pAIState);
        }
    }

    public void ChangeAIMode(EAIMode pMode)
    {
        if(AIMode==pMode)
        {
            return;
        }
        AIMode = pMode;
        switch (pMode)
        {
            case EAIMode.Auto:
                Start();
                break;
            case EAIMode.Hand:
                Stop();
                break;
        }   
    }

    public void Stop()
    {
        this.AIStateMachine.SetCurrState(null);
        this.AIStateMachine.SetGlobalState(null);
    }

    public void Clear()
    {
        AIStateMachine.Clear();
    }
}
