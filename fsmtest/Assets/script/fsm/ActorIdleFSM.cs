using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorIdleFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        Owner.OnIdle();
    }
}

