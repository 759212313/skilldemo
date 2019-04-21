using UnityEngine;
using System.Collections;
using Cfg.Act;
using System.Collections.Generic;
using System;

public class ActSkill : IGame
{
    public int            Id   { get; private set; }
    public ActSkillData   Data { get; private set; }
    public List<ActBase>  Acts { get; private set; }
    private float mStartTimer;
    private bool  mFirstToUse = true;
    private bool  mFinished = false;

    public ActSkill(int id, ActSkillData data)
    {
        this.Id = id;
        this.Data = data;
        this.Init();
    }

    public bool IsInCD()
    {
        return mFirstToUse == false && Time.realtimeSinceStartup - mStartTimer < Data.CD;
    }

    public bool IsFinish()
    {
        return mFinished;
    }

    public void Start()
    {
        mStartTimer = Time.realtimeSinceStartup;
        mFirstToUse = false;
        mFinished = false;
        for (int i = 0; i < Acts.Count; i++)
        {
            Acts[i].Enter();
        }
    }

    public void Step()
    {
        for (int i = 0; i < Acts.Count; i++)
        {
            Acts[i].Execute();
        }
        if (mStartTimer - Time.realtimeSinceStartup > Data.CD)
        {
            mFinished = true;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < Acts.Count; i++)
        {
            Acts[i].Exit();
        }
    }

    public void Init()
    {
        this.Acts = new List<ActBase>();
        for (int i = 0; i < Data.Actions.Count; i++)
        {
            ActData data = Data.Actions[i];
            ActBase act  = ActFactory(data);
            if (act != null)
            {
                Acts.Add(act);
            }
        }
    }

    public static ActBase ActFactory(ActData data)
    {
        ActBase act = null;
        switch (data.Type)
        {
            case EActType.TYPE_ANIM:
                act = new ActAnim();
                break;
        }
        if (act != null)
        {
            act.Data = data;
        }
        return act;
    }
}
