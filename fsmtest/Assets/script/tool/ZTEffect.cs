using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ZTEffect : Singleton<ZTEffect>
{
    private Dictionary<int, EffectBase> mEffectMap = new Dictionary<int, EffectBase>();
    private List<EffectBase> mDelEffectList = new List<EffectBase>();
    private List<EffectBase> mAddEffectList = new List<EffectBase>();
    private int mEffectInstanceID;
    
    public int GetInstanceID()
    {
        mEffectInstanceID++;
        return mEffectInstanceID;
    }

    public GameObject LoadEffectById(int id)
    {
        DBEffect db = ZTConfig.Instance.GetDBEffect(id);
        if (db == null)
        {
            Debug.LogError("DBEffect is null" + id);
            return null;
        }
        GameObject go = ZTPool.Instance.GetGo(db.Path);
        //GameObject go = ZTPool.Instance.GetGo("Effect/1/61009");
        return go;
    }

    public void AddEffect(EffectBase effect)
    {
        mAddEffectList.Add(effect);
    }

    public EffectBase CreateEffect(EffectData data)
    {
        EffectBase effect = new EffectBase(data.Id, GetInstanceID(), data);
        effect.Start();
        AddEffect(effect);
        return effect;
    }

    public void Step()
    {
        for (int i = 0; i < mAddEffectList.Count; i++)
        {
            mEffectMap.Add(mAddEffectList[i].GUID, mAddEffectList[i]);
        }
        mAddEffectList.Clear();
        Dictionary<int, EffectBase>.Enumerator em = mEffectMap.GetEnumerator();
        while (em.MoveNext())
        {
            EffectBase effect = em.Current.Value;
            if (effect.CanUpdate())
            {
                effect.Step();
            }
            if (effect.IsDead())
            {
                mDelEffectList.Add(effect);
            }
        }
        em.Dispose();
        for (int i = 0; i < mDelEffectList.Count; i++)
        {
            EffectBase effect = mDelEffectList[i];
            DestroyEffect(effect);
        }
        mDelEffectList.Clear();
    }

    public void DestroyEffect(EffectBase effect)
    {
        mEffectMap.Remove(effect.GUID);
        effect = null;
    }

    public void DestroyAllEffect()
    {
        Dictionary<int, EffectBase>.Enumerator em = mEffectMap.GetEnumerator();
        while (em.MoveNext())
        {
            EffectBase effect = em.Current.Value;
            effect.Clear();
            effect = null;
        }
        em.Dispose();
        mEffectMap.Clear();
    }

    public void Clear()
    {
        DestroyAllEffect();
        mDelEffectList.Clear();
    }
}
