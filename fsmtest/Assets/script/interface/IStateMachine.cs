using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class IStateMachine<T, F> : IGame where T : class
{
    private T Owner { get; set; }
    private Dictionary<F, IState<T, F>> mStates;
    private IState<T, F> mCurrState;
    private IState<T, F> mPrevState;
    private IState<T, F> mGlobalState;

    public IStateMachine(T owner)
    {
        this.Owner = owner;
        mStates = new Dictionary<F, IState<T, F>>();
    }

    public bool Contains(F fsmID)
    {
        return this.mStates.ContainsKey(fsmID);
    }

    public void AddState(F fsmID, IState<T, F> state)
    {
        if (Contains(fsmID))
        {
            return;
        }
        state.SetFSMID(fsmID);
        state.Owner = Owner;
        mStates.Add(fsmID, state);
    }

    public IState<T, F> GetState(F fsmID)
    {
        if (Contains(fsmID))
        {
            return mStates[fsmID];
        }
        return null;
    }

    public void ChangeState(IState<T, F> newState)
    {
        mPrevState = mCurrState;
        mCurrState.Exit();
        mCurrState = newState;
        mCurrState.Enter();
    }

    public void ChangeState(F newFSM)
    {
        IState<T, F> newState = GetState(newFSM);
        if (newState != null)
        {
            ChangeState(newState);
        }
    }

    public void SetCurrState(IState<T, F> fsm)
    {
        mCurrState = fsm;
    }

    public void SetPrevState(IState<T, F> fsm)
    {
        mPrevState = fsm;
    }

    public void SetGlobalState(IState<T, F> fsm)
    {
        mGlobalState = fsm;
    }

    public F GetCurrStateID()
    {
        if (mCurrState == null)
        {
            return default(F);
        }
        return mCurrState.Fsm;
    }

    public F GetPrevStateID()
    {
        if (mPrevState == null)
        {
            return default(F);
        }
        return mPrevState.Fsm;
    }

    public void Start()
    {
        if (mCurrState != null)
        {
            mCurrState.Enter();
        }
        if (mGlobalState != null)
        {
            mGlobalState.Enter();
        }
    }

    public void Step()
    {
        if (mCurrState != null)
        {
            mCurrState.Execute();
        }
        if (mGlobalState != null)
        {
            mGlobalState.Execute();
        }
    }

    public void Clear()
    {
        if (mCurrState != null)
        {
            mCurrState.Exit();
        }
        if (mGlobalState != null)
        {
            mGlobalState.Exit();
        }
        mCurrState = null;
        mGlobalState = null;
        mStates.Clear();
    }
}
