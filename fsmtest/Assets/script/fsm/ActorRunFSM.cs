using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorRunFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        if (Cmd is RTCommand)
        {
            RTCommand ev = Cmd as RTCommand;
            Owner.OnPursue(ev);
        }
        else
        {
            MVCommand ev = Cmd as MVCommand;
            Owner.OnForceToMove(ev);
        }
    }

}
