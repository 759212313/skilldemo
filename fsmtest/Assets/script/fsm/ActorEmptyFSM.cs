using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorEmptyFSM : ActorBaseFSM
{

    public override void Enter()
    {
        base.Enter();
        Owner.SendStateMessage(FSMState.FSM_IDLE);
    }
}
