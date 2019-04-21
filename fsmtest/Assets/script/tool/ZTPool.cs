using System;
using System.Collections.Generic;
using UnityEngine;

public enum EPoolObjectType
{
    PEffect,
    PTrail,
    PEntity,
    PBoard,
    PMesh
}

public class XPoolObj
{
    public GameObject gameObject;
    public string name;
    public EPoolObjectType type;
}

public class PoolInfo
{
    public Queue<XPoolObj> quene = new Queue<XPoolObj>();
}

public class ZTPool : MonoSingleton<ZTPool>
{
    private Dictionary<string, PoolInfo> mPoolDict = new Dictionary<string, PoolInfo>();
    private GameObject mObjectsPool;
    private List<GameObject> mDestroyPoolGameObjects = new List<GameObject>();//删除队列

    private bool TryGetObject(PoolInfo poolInfo,ref XPoolObj obj)
    {
        if(poolInfo.quene.Count>0)
        {
            obj=poolInfo.quene.Dequeue();
            return true;
        }
        return false;
    }

    public GameObject GetGo(string path)
    {
        if(string.IsNullOrEmpty(path))
        {
            return null;
        }
        PoolInfo poolInfo = null;
        XPoolObj obj = null;
        if(mPoolDict.ContainsKey(path))
        {
            poolInfo = mPoolDict[path];
            if(TryGetObject(poolInfo,ref obj))
            {
                EnablePoolGameObject(obj.gameObject, obj);
                return obj.gameObject;
            }
        }
        return ZTResource.Instance.Instantiate(path);
    }

    public void EnablePoolGameObject(GameObject go,XPoolObj obj)
    {
        go.SetActive(true);
        go.transform.parent = null;    
    }

    public void DisablePoolGameObject(GameObject go, XPoolObj info)
    {
        go.SetActive(false);
    }

    public void Clear()
    {
        mPoolDict.Clear();
        mDestroyPoolGameObjects.Clear();
        mObjectsPool = null;
    }

    public void ReleaseGo(string path,GameObject go,EPoolObjectType type)
    {
        if (string.IsNullOrEmpty(path) || go == null)
        {
            return;
        }
        if (mObjectsPool==null)
        {
            mObjectsPool = new GameObject("PoolManager");
            mObjectsPool.transform.localPosition = new Vector3(0, -10000, 0);
        }
        go.transform.parent = mObjectsPool.transform;
        PoolInfo poolInfo = null;
        if(!mPoolDict.TryGetValue(path,out poolInfo))
        {
            poolInfo = new PoolInfo();
            mPoolDict.Add(path, poolInfo);
        }
        XPoolObj obj = new XPoolObj();
        obj.type = type;
        obj.name = path;
        obj.gameObject = go;
        DisablePoolGameObject(go, obj);
        poolInfo.quene.Enqueue(obj);
    }
}