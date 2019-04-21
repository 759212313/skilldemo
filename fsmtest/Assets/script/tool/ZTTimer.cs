using System;
using System.Collections.Generic;
using UnityEngine;


public class Timer
{
    public int key;
    public Callback callback;
    public float callTime;
    public float startTime;
    public int tick;
    public int currTick;
    public float currTime;

    public bool pause = false;

    public float GetLeftTime()
    {
        return currTime - startTime > 0 ? currTime - startTime : 0;
    }
}

public class ZTTimer : Singleton<ZTTimer>, IGame
{
    private Dictionary<int, Timer> mTimers = new Dictionary<int, Timer>();
    private List<Timer> mAddBuffer = new List<Timer>();
    private List<int> mDeleteBuffer = new List<int>();
    private int mIndex = 0;

    public Timer Register(float callTime,Callback callback,int tick=1)
    {
        if(callback==null)
        {
            return null;
        }
        if(callTime<=0)
        {
            return null;
        }
        mIndex++;
        Timer item = new Timer();
        item.key = mIndex;
        item.callTime = callTime;
        item.callback = callback;
        item.tick = tick;
        item.currTick = 0;
        item.startTime = Time.realtimeSinceStartup;
        item.currTime = item.startTime;
        mAddBuffer.Add(item);
        return item;
    }

    public void UnRegister(Callback callback)
    {
        Dictionary<int, Timer>.Enumerator em = mTimers.GetEnumerator();
        while (em.MoveNext())
        {
            if(em.Current.Value.callback==callback)
            {
                mDeleteBuffer.Add(em.Current.Key);
            }
        }
        em.Dispose();
    }

    public void UnRegister(Timer timer)
    {
        if(timer==null)
        {
            return;
        }
        mDeleteBuffer.Add(timer.key);
    }

    public void Step()
    {
        for(int i=0;i<mAddBuffer.Count;i++)
        {
            Timer item = mAddBuffer[i];
            mTimers.Add(item.key, item);
        }
        mAddBuffer.Clear();

        Dictionary<int, Timer>.Enumerator em = mTimers.GetEnumerator();
        while (em.MoveNext())
        {
            Timer item = em.Current.Value;
            item.currTime = Time.realtimeSinceStartup;
            if (Time.realtimeSinceStartup - item.startTime >= item.callTime)
            {
                if (item.callback != null)
                {
                    item.callback();
                }
                item.startTime = Time.realtimeSinceStartup;
                if (item.tick > 0)
                {
                    item.currTick++;
                    if (item.tick == item.currTick)
                    {
                        mDeleteBuffer.Add(item.key);
                    }
                }
            }
            if(item.pause==true)
            {
                mDeleteBuffer.Add(item.key);
            }
        }
        em.Dispose();
        for (int i=0;i<mDeleteBuffer.Count;i++)
        {
            mTimers.Remove(mDeleteBuffer[i]);
        }
        mDeleteBuffer.Clear();
    }

    public void Clear()
    {
        mTimers.Clear();
        mAddBuffer.Clear();
        mDeleteBuffer.Clear();
    }

    public void Start()
    {
 
    }
}