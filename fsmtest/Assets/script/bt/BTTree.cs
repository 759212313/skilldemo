using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.IO;


namespace BT
{

    public class BTTree : BTNode
    {
        public Actor Owner { get; set; }
        public int Id { get; set; }
        protected Dictionary<string, object> mDataMap = new Dictionary<string, object>();

        public virtual void Start()
        {
            BTTreeManager.Instance.Run(this);
        }

        public override EBTStatus Step()
        {
            if (FirstChild == null)
            {
                return EBTStatus.BT_FAILURE;
            }
            return FirstChild.Step();
        }

        public void FindAllChildren(BTNode pNode, List<BTNode> pList)
        {
            pList.AddRange(pNode.Children);
            for (int i = 0; i < pNode.Children.Count; i++)
            {
                BTNode node = pNode.Children[i];
                FindAllChildren(node, pList);
            }
        }

        public override void Break()
        {
            BTTreeManager.Instance.Remove(this);
        }

        public void SetData(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            mDataMap[key] = value;
        }

        public object GetData(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            object var = null;
            mDataMap.TryGetValue(key, out var);
            return var;
        }
    }
}