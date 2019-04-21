using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorSkillFSM : ActorBaseFSM
{
    public override void Enter()
    {
        base.Enter();
        USCommand ev = Cmd as USCommand;
        //Debug.LogError(ev.LastTime);
        if(ev.LastTime>0)
        {
            ZTTimer.Instance.Register(ev.LastTime, Break);
        }
        Owner.ApplyRootMotion(false);
        Owner.OnUseSkill(ev);
    }

    public override void Exit()
    {
        base.Exit();
        Owner.ApplyRootMotion(true);
    }
}
