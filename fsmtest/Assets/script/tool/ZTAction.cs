using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ZTAction : IGame
{
    private Animator mAnimator;
    private string mAnimName;
    private float mPlaySpeed = 1f;
    private float mPlayTimer = 0f;

    private AnimatorStateInfo mAnimatorState;
    private CAnimator mCAnimator;

    private Map<string, AnimationClip> mAnimClips = new Map<string, AnimationClip>();
    private static HashSet<string> mNoFadeList = new HashSet<string>
    {
        "idle",
        "run",
        "walk"
    };

    struct CAnimator
    {
        public string name;
        public Callback callback;
        public Callback onFinish;
        public float length;
        public bool isStart;
        public bool isLoop;
    }

    public ZTAction(Animator animator)
    {
        this.mAnimator = animator;
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            return;
        }
        this.mCAnimator.isStart = false;
        for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
            mAnimClips[clip.name] = clip;
        }
        mActions[animator] = this;
    }

    public void SetSpeed(float speed)
    {
        mPlaySpeed = speed;
    }

    public void Play(string animName, Callback onFinish = null, bool isLoop = false, float speed = 1f, float lastTime = 0f)
    {
        if (mAnimName == animName || mAnimator == null || !mAnimator.enabled || string.IsNullOrEmpty(animName))
        {
            return;
        }
        if (!string.IsNullOrEmpty(mAnimName))
        {
            mAnimator.SetBool(mAnimName, false);
        }
        mAnimator.SetBool(animName, true);

        if (!mNoFadeList.Contains(mAnimName) && isLoop == false)
        {
            mAnimator.CrossFade(animName, 0.3f);
        }

        mPlayTimer = Time.realtimeSinceStartup;
        mPlaySpeed = speed;
        mAnimName = animName;
        mCAnimator.onFinish = onFinish;
        mCAnimator.name = animName;
        mCAnimator.isStart = false;
        mCAnimator.isLoop = isLoop;
        mCAnimator.length = lastTime > 0 ? lastTime : GetAnimLength(mAnimName);
    }

    public void Start()
    {

    }

    public void Step()
    {
        if (!mAnimator.enabled)
        {
            return;
        }
        mAnimatorState = mAnimator.GetNextAnimatorStateInfo(0);

        if (!mCAnimator.isStart)
        {
            if (mAnimatorState.IsName(mAnimName))
            {
                mAnimator.SetBool(mAnimName, false);
                mCAnimator.isStart = true;
            }
        }

        if (mCAnimator.onFinish == null)
        {
            return;
        }
        if (Time.realtimeSinceStartup - mPlayTimer > mCAnimator.length)
        {
            mCAnimator.onFinish();
        }
    }

    public void Reset()
    {
        mAnimName = string.Empty;
    }

    public string GetCurrAnimName()
    {
        return mAnimName;
    }

    public Single GetSpeed()
    {
        return mPlaySpeed;
    }

    public float GetAnimLength(string animName)
    {
        if (mAnimClips.ContainsKey(animName))
        {
            return mAnimClips[animName].length;
        }
        return 0;
    }

    public void Break()
    {
        mCAnimator.onFinish = null;
        if (string.IsNullOrEmpty(mAnimName))
        {
            return;
        }
        if (mAnimator != null)
        {
            mAnimator.SetBool(mAnimName, false);
        }
    }

    public void Clear()
    {
        Reset();
        mDeleteList.Add(this);
    }

    private static Dictionary<Animator, ZTAction> mActions = new Dictionary<Animator, ZTAction>();
    private static List<ZTAction> mDeleteList = new List<ZTAction>();

    public static void Update()
    {
        Dictionary<Animator, ZTAction>.Enumerator em = mActions.GetEnumerator();
        while (em.MoveNext())
        {
            Animator animator = em.Current.Key;
            if (animator == null)
            {
                mDeleteList.Add(em.Current.Value);
            }
            else
            {
                em.Current.Value.Step();
            }
        }
        em.Dispose();
        while (mDeleteList.Count > 0)
        {
            Animator animator = mDeleteList[0].mAnimator;
            mActions.Remove(animator);
            mDeleteList.RemoveAt(0);
        }
    }

    public static ZTAction Get(Animator animator)
    {
        if (animator == null)
        {
            return null;
        }
        ZTAction action = null;
        mActions.TryGetValue(animator, out action);
        if (action == null)
        {
            action = new ZTAction(animator);
        }
        return action;
    }

    public static void Remove(ZTAction action)
    {
        if (action == null)
        {
            return;
        }
        mDeleteList.Add(action);
    }


}