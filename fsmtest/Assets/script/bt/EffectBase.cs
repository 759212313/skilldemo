using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EffectData
{
    public int Id;
    public Single Delay;
    public Single LastTime;
    public Single FlySpeed;

    public EFlyType Fly = EFlyType.STAY;
    public EFlyObjDeadType Dead = EFlyObjDeadType.UntilLifeTimeEnd;
    public EEffectBind Bind = EEffectBind.OwnBody;

    public Vector3 Offset;
    public Vector3 Euler;

    public Actor Target;//特效移动朝向
    public Actor Owner; //释放特效的人
    public Transform Parent;
    public bool SetParent = false;
    public string Sound = string.Empty;
}


public class EffectBase : IEntiny,IGame
{
    public GameObject Obj { get; set; }
    public int GUID { get; set; }
    public int Id { get; set; }
    public Transform CacheTransform { get; set; }
    public EEffectState State { get; private set; }
    protected EffectData  mData;
    protected Vector3 mTargetPos;
    protected Vector3 mStartPos;
    protected Actor mTarget;
    protected Actor mOwner; 

    protected float mUpdateTime;
    protected Timer mDelayTimer = null;

    public EffectBase(int id, int guid,EffectData data)
    {
        this.Id = id;
        this.GUID = guid;
        this.mData = data;
        this.mTarget = data.Target;
        this.mOwner = data.Owner;
        if (data.Owner==null||data.Owner.CacheTransform==null)
        {
            this.SwitchState(EEffectState.Error);
        }
        else
        {
            this.SwitchState(EEffectState.Wait);
        }
    }

    public void Load(XTransform param)
    {
        GameObject go = ZTEffect.Instance.LoadEffectById(Id);
        if (go == null || mOwner == null || mOwner.CacheTransform == null)
        {
            this.State = EEffectState.Error;
            return;
        }
        this.Obj = go;
        this.CacheTransform = go.transform;

        switch (mData.Bind)
        {
            case EEffectBind.World:
                {
                    CacheTransform.position = mData.Offset;
                    CacheTransform.eulerAngles = mData.Euler;
                }
                break;
            case EEffectBind.Trans:
                {
                    if (mData.Parent == null)
                    {
                        CacheTransform.position = mData.Offset;
                        CacheTransform.eulerAngles = mData.Euler;
                    }
                    else
                    {
                        mData.SetParent = true;
                        CacheTransform.position = mData.Parent.position + mData.Offset;
                        CacheTransform.eulerAngles = mData.Parent.eulerAngles + mData.Euler;
                    }
                }
                break;

            case EEffectBind.OwnBody:
                {
                    CacheTransform.eulerAngles = mOwner.CacheTransform.localEulerAngles + mData.Euler;
                    CacheTransform.position = mOwner.GetBind(EActorBindPos.Body, mData.Offset);
                }
                break;
            case EEffectBind.OwnHead:
                {
                    CacheTransform.eulerAngles = mOwner.CacheTransform.localEulerAngles + mData.Euler;
                    CacheTransform.position = mOwner.GetBind(EActorBindPos.Head, mData.Offset);
                }
                break;
            case EEffectBind.OwnFoot:
                {
                    CacheTransform.eulerAngles = mOwner.CacheTransform.localEulerAngles + mData.Euler;
                    CacheTransform.position = mOwner.GetBind(EActorBindPos.Foot, mData.Offset);
                }
                break;
            case EEffectBind.OwnHand:
                {
                    mData.SetParent = true;
                    Transform[] hands = mOwner.GetHands();
                    if (hands != null && hands[0] != null)
                    {
                        mData.Parent = hands[0];
                        CacheTransform.position = mData.Parent.position + mData.Offset;
                        CacheTransform.eulerAngles = mData.Parent.eulerAngles + mData.Euler;
                    }
                    else
                    {
                        CacheTransform.position = mData.Offset;
                        CacheTransform.eulerAngles = mData.Euler;
                    }
                }
                break;
            case EEffectBind.TarBody:
                {
                    Actor actor = mTarget == null ? mOwner : mTarget;
                    CacheTransform.position = actor.GetBind(EActorBindPos.Head, mData.Offset);
                }
                break;
            case EEffectBind.TarFoot:
                {
                    Actor actor = mTarget == null ? mOwner : mTarget;
                    CacheTransform.position = actor.GetBind(EActorBindPos.Foot, mData.Offset);
                }
                break;
            case EEffectBind.TarHead:
                {
                    Actor actor = mTarget == null ? mOwner : mTarget;
                    CacheTransform.position = actor.GetBind(EActorBindPos.Head, mData.Offset);
                }
                break;
            case EEffectBind.OwnVTar:
                {
                    if (mOwner != null && mTarget != null)
                    {
                        Vector3 pos1 = mTarget.GetBind(EActorBindPos.Body, Vector3.zero);
                        Vector3 pos2 = mOwner.GetBind(EActorBindPos.Body, Vector3.zero);
                        CacheTransform.position = (pos1 - pos2) * 0.5f + pos2;
                    }
                    else
                    {
                        CacheTransform.position = mOwner.GetBind(EActorBindPos.Body, Vector3.zero) + mOwner.Dir * 10;
                    }
                }
                break;
        }

        if (mData.SetParent && mData.Parent != null)
        {
            CacheTransform.parent = mData.Parent;
        }
        mStartPos = CacheTransform.position;
        ZTAudio.Instance.PlayEffectAudio(mData.Sound);
        this.State = EEffectState.Update;
    }

    public void Destroy()
    {

    }

    public bool IsDead()
    {
        return State == EEffectState.Error || State == EEffectState.Dead;
    }

    public bool IsDestroy()
    {
        return CacheTransform == null || Obj == null;
    }

    public void Start()
    {
        if(mData.Delay>Define.MIN_INTERVAL_VALUE)
        {
            SwitchState(EEffectState.Wait);
            mDelayTimer = ZTTimer.Instance.Register(mData.Delay, () => { Load(null); });
        }
        else
        {
            Load(null);
            SwitchState(EEffectState.Update);
        }
    }

    public void Step()
    {
        if (this.State != EEffectState.Update||CacheTransform==null)
        {
            return;
        }
        switch (mData.Fly)
        {
            case EFlyType.LINE:
                {
                    MoveExecuteLine();
                }
                break;
            case EFlyType.CROSS:
                {
                    MoveExecuteLine();
                }
                break;
            case EFlyType.THROW:
                {
                    MoveExecuteThrow();
                }
                break;
            case EFlyType.BACK:
                {
                    MoveExecuteGoBack();
                }
                break;
            case EFlyType.PURSUE:
                {
                    MoveExecutePursue();
                }
                break;
        }

        if (mData.LastTime > 0)
        {
            if (mUpdateTime < mData.LastTime)
            {
                mUpdateTime += Time.deltaTime;
            }
            else
            {
                SwitchState(EEffectState.Dead);
            }
        }
    }

    public void Clear()
    {
        ZTTimer.Instance.UnRegister(mDelayTimer);
        if(Obj!=null)
        {
            string path = ZTConfig.Instance.GetDBEffect(Id).Path;
            //string path = "Effect/1/61009";
            ZTPool.Instance.ReleaseGo(path, Obj, EPoolObjectType.PEffect);
            Obj = null;
            CacheTransform = null;
        }
    }

    public bool CanUpdate()
    {
        return State == EEffectState.Update;
    }  

    public void MoveExecuteLine()
    {
        MoveByDir(CacheTransform.forward);
    }

    public void MoveExecuteThrow()
    {

    }

    public void MoveExecuteGoBack()
    {
     
    }

    public void MoveExecutePursue()
    {
        if (mTarget==null||mTarget.CacheTransform==null)
        {
            MoveByDir(CacheTransform.forward);
        }
        else
        {
            MoveTo(CacheTransform.position, mTarget.GetBind(EActorBindPos.Body, Vector3.zero));
        }
    }

    public void MoveExecuteCross()
    {

    }

    public void MoveByDir(Vector3 dict)
    {
        Vector3 dir = dict;
        dir.Normalize();
        if(dir==Vector3.zero)
        {
            return;
        }
        CacheTransform.rotation = Quaternion.LookRotation(dir);
        Vector3 move = dir * Time.deltaTime * mData.FlySpeed;
        CacheTransform.position += move;
    }

    public void MoveTo(Vector3 from,Vector3 to)
    {
        MoveByDir(to-from);
    }

    public void Reset()
    {
        mUpdateTime = 0;
    }

    public void Pause(bool pause)
    {
  
    }

    public void SwitchState(EEffectState state)
    {
        State = state;
        if(state==EEffectState.Dead)
        {
            Clear();
        }
    }
}
