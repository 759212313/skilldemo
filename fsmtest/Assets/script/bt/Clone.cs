using UnityEngine;
using System.Collections;
using System;
using System.Xml;

namespace BT
{
    public class Clone : Composite
    {
        public Int32 Times = 1;
        public float Interval = 0;
        protected Timer mTimer;
        protected Int32 mCloneIndex = 0;

        protected override void ReadAttribute(string key, string value)
        {
            switch (key)
            {
                case "Times":
                    this.Times = value.ToInt32();
                    break;
                case "Interval":
                    this.Interval = value.ToFloat();
                    break;
            }
        }

        protected override void SaveAttribute(System.Xml.XmlDocument doc, XmlElement xe)
        {
            base.SaveAttribute(doc, xe);
            xe.SetAttribute("Times", Times >= 1 ? Times.ToString() : "1");
            xe.SetAttribute("Interval", Interval.ToString());
        }

        public override EBTStatus Step()
        {
            if (Times < 1 || Interval < 0)
            {
                return EBTStatus.BT_FAILURE;
            }

            if (mCloneIndex == 0)
            {
                if (Interval > 0)
                {
                    mTimer = ZTTimer.Instance.Register(Interval, DoForeach, Times);
                }
                else
                {
                    for (int i = 0; i < Times; i++)
                    {
                        DoForeach();
                    }
                }
            }
            return mCloneIndex >= Times ? EBTStatus.BT_SUCCESS : EBTStatus.BT_RUNNING;
        }

        public override void Clear()
        {
            base.Clear();
            mCloneIndex = 0;
            ZTTimer.Instance.UnRegister(DoForeach);
        }

        protected void DoForeach()
        {
            mCloneIndex++;
            CloneTree tree = new CloneTree();
            tree.Owner = BTTreeManager.Instance.GetOwnerByNode(this);
            tree.CloneChildren(this);
            BTTreeManager.Instance.Run(tree);
        }

        public override BTNode DeepClone()
        {
            Clone clone = new Clone();
            clone.Times = this.Times;
            clone.Interval = this.Interval;
            return clone;
        }
    }
}