using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

namespace Cfg.Act
{
    public class ActData : CfgObj
    {
        public EActType Type = EActType.TYPE_NONE;
        public float Time = 0;
        public ParamBase   ActionParam;
        public ParamAffect AffectParam;
        public bool Warning = false;
        public List<ActData> Actions = new List<ActData>();

        public override void Read(XmlElement os)
        {
            for (int i = 0; i < os.Attributes.Count; i++)
            {
                XmlAttribute child = os.Attributes[i] as XmlAttribute;
                if (child == null) continue;
                switch (child.Name)
                {
                    case "Type":
                        this.Type = (EActType)Enum.Parse(typeof(EActType), child.Value);
                        break;
                    case "Time":
                        this.Time = ReadFloat(child.Value);
                        break;
                    case "Action":
                        break;
                    case "Affect":
                        this.AffectParam = new ParamAffect();
                        this.AffectParam.Parse(child.Value);
                        break;
                    case "Warning":
                        this.Warning = ReadBool(child.Value);
                        break;
                }
            }
            XmlNode pNode = os.FirstChild;
            while (pNode != null)
            {
                ActData data = new ActData();
                data.Read(pNode as XmlElement);
                Actions.Add(data);
                pNode = pNode.NextSibling;
            }
        }

        public override void Write(XmlDocument doc, XmlElement os)
        {
            XmlElement child = doc.CreateElement("Act");
            Write(doc, os, "Type", Type.ToString());
            Write(doc, os, "Time", Time);
            if (this.ActionParam != null)
            {
                string s = string.Empty;
                ActionParam.Parse(s);
                Write(doc, os, "Action", s);
            }
            if (this.AffectParam != null)
            {
                string s = string.Empty;
                AffectParam.Parse(s);
                Write(doc, os, "Affect", s);
            }
            Write(doc, os, "Warning", Warning);
            for (int i = 0; i < Actions.Count; i++)
            {
                Actions[i].Write(doc, os);
            }
        }
    }
}

