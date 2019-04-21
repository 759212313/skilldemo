using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using BT;

public class BTTreeManager : Singleton<BTTreeManager>
{
    private List<BTTree> mBTTrees = new List<BTTree>();
    private List<BTTree> mDeleteList = new List<BTTree>();

    public void Run(BTTree tree)
    {
        if (tree == null) return;
        tree.Clear();
        mBTTrees.Add(tree);
    }

    public void Clear()
    {
        for (int i = 0; i < mBTTrees.Count; i++)
        {
            BTTree tree = mBTTrees[i];
            tree.Clear();
        }
        mBTTrees.Clear();
    }

    public void Step()
    {
        for (int i = 0; i < mBTTrees.Count; i++)
        {
            BTTree tree = mBTTrees[i];
            EBTStatus pStatus = tree.Step();

            switch (pStatus)
            {
                case EBTStatus.BT_INITIAL:
                    break;
                case EBTStatus.BT_RUNNING:
                    break;
                case EBTStatus.BT_SUCCESS:
                case EBTStatus.BT_FAILURE:
                    {
                        mDeleteList.Add(tree);
                    }
                    break;
            }
        }

        for (int i = 0; i < mDeleteList.Count; i++)
        {
            BTTree tree = mDeleteList[i];
            mBTTrees.Remove(tree);
            tree.Clear();
        }
        mDeleteList.Clear();
    }

    public void GetTreeInParents(BTNode pNode, ref BTTree result)
    {
        if (pNode == null)
        {
            return;
        }
        if (pNode.Parent is BTTree)
        {
            result = pNode.Parent as BTTree;
        }
        else
        {
            GetTreeInParents(pNode.Parent, ref result);
        }
    }

    public BTTree GetFirstTreeInParent(BTNode pNode)
    {
        BTTree result = null;
        GetTreeInParents(pNode, ref result);
        if (result == null)
        {
            Debug.LogError("找不到BTree类型的Parent：" + pNode);
            return null;
        }
        return result;
    }

    public void SaveData(BTNode pNode, string key, object value)
    {
        BTTree result = GetFirstTreeInParent(pNode);
        if (result != null)
        {
            result.SetData(key, value);
        }
    }

    public void ClearData(BTNode pNode, string key)
    {
        BTTree result = GetFirstTreeInParent(pNode);
        if (result != null)
        {
            result.SetData(key, null);
        }
    }

    public object GetData(BTNode pNode, string key)
    {
        BTTree result = GetFirstTreeInParent(pNode);
        if (result != null)
        {
            return result.GetData(key);
        }
        return null;
    }

    public Actor GetOwnerByNode(BTNode pNode)
    {
        BTTree tree = GetFirstTreeInParent(pNode);
        if (tree == null)
        {
            return null;
        }
        return tree.Owner;
    }

    public void Remove(BTTree tree)
    {
        if (tree == null)
        {
            return;
        }
        mDeleteList.Add(tree);
    }
}
