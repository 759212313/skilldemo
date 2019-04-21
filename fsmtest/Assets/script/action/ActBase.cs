using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cfg.Act;
using BT;

public abstract class ActBase
{
    public int ID;
    public EActType Type = EActType.TYPE_NONE;
    public Actor   Caster;
    public ActData Data;

    public virtual void Enter() { }
    public virtual EActStatus Execute() { return EActStatus.RUNNING; }
    public virtual void Exit() { }
}
