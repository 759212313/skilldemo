using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardBase: MonoBehaviour
{
    protected string mResPath;
    protected bool mNeedUpdate = true;
    protected float mHeight = 0f;
    protected Transform mTarget;
    public string Path = string.Empty;
    public object Owner { get; protected set; }


    void Update()
    {
        if (mNeedUpdate == false)
        {
            return;
        }
        if (mTarget == null)
        {
            return;
        }
        UpdatePosition(mTarget);
    }

    public void SetOwner(object owner)
    {
        this.Owner = owner;
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    public void SetVisbale(bool val)
    {
        gameObject.SetActive(val);
    }

    public void UpdatePosition(Transform target)
    {
        //if (ZTCamera.Instance.MainCamera == null|| ZTCamera.Instance.NGUICamera == null)
        //{
        //    return;
        //}


        Vector3 pos_3d = target.position + new Vector3(0, mHeight, 0);
        Vector2 pos_screen = Laucher.instance.MainCamera.WorldToScreenPoint(pos_3d);
        Vector3 pos_ui = Laucher.instance.NGUICamera.ScreenToWorldPoint(pos_screen);
        //if (target.name != "1(Clone)")
        //{
        //    Debug.LogError("====1===" + target.position.x + " " + target.position.y + " " + target.position.z+" "+ "====2===" + pos_ui.x + " " + pos_ui.y + " " + pos_ui.z);
        //}

        transform.position =  Vector3.Slerp(transform.position, pos_ui,Time.time*5);
    }

    public virtual void Init()
    {

    }

    public virtual void ResetValue()
    {

    }

    public virtual void Refresh()
    {

    }

    public void Release()
    {
        this.enabled = false;
        ZTPool.Instance.ReleaseGo(Path, gameObject, EPoolObjectType.PBoard);
    }
}
