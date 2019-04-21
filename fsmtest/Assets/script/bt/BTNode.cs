using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Reflection;

namespace BT
{
    public enum EBTStatus
    {
        BT_INITIAL,
        BT_SUCCESS,
        BT_FAILURE,
        BT_RUNNING,
    }

    public enum EBTCheck
    {
        CH_SUCCESS,
        CH_FAILURE,
    }

    public class BTNode
    {
        protected List<BTNode> mChildren = new List<BTNode>();
        protected BTNode mParent;
        protected BTNode mFirstChild;
        protected bool mIsRunning = false;

        public BTNode GetChild(int index)
        {
            if (index < this.mChildren.Count)
            {
                return this.mChildren[index];
            }
            return null;
        }

        public virtual EBTStatus Step()
        {
            return EBTStatus.BT_FAILURE;
        }

        public virtual void Clear()
        {
            mIsRunning = false;
            for(int i=0;i<mChildren.Count;i++)
            {
                mChildren[i].Clear();
            }
        }

        public virtual bool CheckCondition()
        {
            return false;
        }

        public void AddChild(BTNode pChild)
        {
            pChild.mParent = this;
            this.mChildren.Add(pChild);
        }

        public void DelChild(BTNode pChild)
        {
            this.mChildren.Remove(pChild);
        }

        public virtual void Load(XmlElement xe)
        {
            //Util.Instance.Log("=======加载BT数据======");
            if (xe == null)
            {
                Debug.LogError("XmlElement is null" + this.GetType().ToString());
                return;
            }
            for (int i = 0; i < xe.Attributes.Count; i++)
            {
                XmlAttribute attr = xe.Attributes[i];
                this.ReadAttribute(attr.Name, attr.Value);
            }
            XmlNode child = xe.FirstChild;
            while (child != null)
            {
                Type classType = null;
                try
                {
                    classType = Type.GetType("BT." + child.Name, true);
                }
                catch
                {
                    Debug.LogError("XmlElement is null:" + this.GetType().ToString()+"."+child.Name);
                    break;
                }

                BTNode node = Activator.CreateInstance(classType) as BTNode;
                if(node==null)
                {
                    Debug.LogError(classType);
                    return;
                }
                node.Load(child as XmlElement);
                this.AddChild(node);
                child = child.NextSibling;
            }
        }

        public virtual void Save(XmlDocument doc, XmlElement xe)
        {
            this.SaveAttribute(doc, xe);
            for (int i = 0; i < mChildren.Count; i++)
            {
                BTNode node = mChildren[i];
                XmlElement child = doc.CreateElement(node.GetType().Name);
                node.Save(doc, child);
                xe.AppendChild(child);
            }
        }

        public virtual void Init() { }

        public virtual void Break() { }

        protected virtual void ReadAttribute(string key, string value) { }

        protected virtual void SaveAttribute(XmlDocument doc, XmlElement xe) { }


        public int GetChildCount()
        {
            return mChildren.Count;
        }

        public BTNode Parent
        {
            get { return mParent; }
        }

        public BTNode FirstChild
        {
            get
            {
                if (mFirstChild == null)
                {
                    mFirstChild = mChildren.Count > 0 ? mChildren[0] : null;
                }
                return mFirstChild;
            }
        }

        public List<BTNode> Children
        {
            get { return mChildren; }
        }

        public bool IsRunning
        {
            get { return mIsRunning; }
        } 

        public virtual BTNode DeepClone()
        {
            Debug.LogError("clone object error:"+this.GetType().ToString());
            return null;
        }

        public void CloneChildren(BTNode pNode)
        {
            for(int i=0;i<pNode.Children.Count;i++)
            {
                BTNode node = pNode.Children[i];
                BTNode newNode = node.DeepClone();
                AddChild(newNode);
            }
        }
    }
}
