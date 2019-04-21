using UnityEngine;
using System.Collections;
using System.Xml;

namespace BT
{

    public class Wait : Action
    {
        public float Seconds;
        private float mClocker;

        protected override bool Enter()
        {
            mClocker = Time.realtimeSinceStartup;
            return Seconds > 0;
        }

        protected override void ReadAttribute(string key, string value)
        {
            switch (key)
            {
                case "Seconds":
                    this.Seconds = value.ToFloat();
                    break;
            }
        }

        protected override void SaveAttribute(XmlDocument doc, XmlElement xe)
        {
            xe.SetAttribute("Seconds", Seconds.ToString());
        }

        protected override EBTStatus Execute()
        {
            if (Time.realtimeSinceStartup - mClocker > Seconds)
            {
                return EBTStatus.BT_SUCCESS;
            }
            return EBTStatus.BT_RUNNING;
        }

        public override void Clear()
        {
            base.Clear();
            mClocker = 0;
        }

        public override BTNode DeepClone()
        {
            Wait node = new Wait();
            node.Seconds = this.Seconds;
            return node;
        }
    }
}
