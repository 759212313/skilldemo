using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ActorPlayer : Actor
{
    public ActorPlayer(int id, int guid, EActorType type, EBattleCamp camp) :base(id, guid, type,camp)
    {

    }

    /*public override void OnBeginRide()
    {
        this.StopPathFinding();
        this.LoadMount();
        mMount.SetHost(this);
        mActorAction.Play("qicheng",null,true);
        mCharacter.enabled = false;
        this.SetActorEffect(EActorEffect.Is_Ride, true);
    }

    public override void OnEndRide()
    {
        CacheTransform.parent = ZTLevel.Instance.GetHolder(EMapHolder.Role).transform;
        CacheTransform.localPosition = GTTools.NavSamplePosition(Pos);
        mCharacter.enabled = true;
        if (mMount!=null)
        {
            ZTLevel.Instance.DelActor(mMount);
            mMount = null;
        }
        mVehicle = this;
        this.SetActorEffect(EActorEffect.Is_Ride, false);
        this.SendStateMessage(FSMState.FSM_IDLE);
    }

    public override void MoveTo(Vector3 destPosition)
    {
        if (mVehicle != this)
        {
            mVehicle.MoveTo(destPosition);
            mActorAction.Play("qicheng_run", null, true);
        }
        else
        {
            base.MoveTo(destPosition);
        }
    }

    public override void StopPathFinding()
    {
        if (mVehicle != this)
        {
            mVehicle.StopPathFinding();
        }
        else
        {
            base.StopPathFinding();
        }
    }

    public override void OnForceToMove(MVCommand ev)
    {
        if (mVehicle != this)
        {
            mVehicle.OnForceToMove(ev);
            mActorAction.Play("qicheng_run", null, true);
        }
        else
        {
            base.OnForceToMove(ev);
        }
    }

    public override void OnPursue(RTCommand ev)
    {
        if (mVehicle != this)
        {
            mVehicle.OnPursue(ev);
            mActorAction.Play("qicheng_run", null, true);
        }
        else
        {
            base.OnPursue(ev);
        }
    }

    public override void OnIdle()
    {
        if (mVehicle!=this)
        {
            mVehicle.OnIdle();
            mActorAction.Play("qicheng", null, true);
        }
        else
        {
            base.OnIdle();
        }
    }*/
}