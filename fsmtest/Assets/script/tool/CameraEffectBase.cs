using UnityEngine;
using System.Collections;

public class CameraEffectBase : MonoBehaviour
{
    protected Camera       mCamera;
    protected ECameraType  mType;
    protected ECameraState mState;
    protected float mStartTime = 0;
    protected float mLastTime = 0;
    protected CameraEvent mCallback;
    protected object[] mArgs;

    public virtual void Init(int id, Camera cam, CameraEvent callback, params object[] args)
    {
        this.mCallback = callback;
        this.mArgs = args;
        this.mCamera = cam;
        this.SwitchState(ECameraState.ENTER);
    }

    public virtual void OnEnter()
    {
        this.enabled = true;
        this.mStartTime = Time.realtimeSinceStartup;
        this.SwitchState(ECameraState.UPDATE);
    }

    public virtual void OnLeave()
    {
        this.enabled = false;
        this.mCamera = null;
        this.mCallback = null;
        this.mArgs = null;
        this.SwitchState(ECameraState.NONE);
    }

    public virtual void OnUpdate()
    {

    }

    public void SwitchState(ECameraState state)
    {
        this.mState = state;
        switch (mState)
        {
            case ECameraState.NONE:
                break;
            case ECameraState.ENTER:
                OnEnter();
                break;
            case ECameraState.UPDATE:
                break;
            case ECameraState.LEAVE:
                OnLeave();
                break;
        }
    }

    public ECameraType Type
    {
        get { return mType; }
    }

    void LateUpdate()
    {
        if(mState!=ECameraState.UPDATE)
        {
            return;
        }
        OnUpdate();
    }

}
