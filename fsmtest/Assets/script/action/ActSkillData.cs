using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

namespace Cfg.Act
{
    public class ActSkillData : CfgObj
    {
        public Int32  Id;
        public string Name = string.Empty;
        public float  Dur = 1.0f;
        public float  Speed = 1.0f;
        public float  CD = 0;
        public float  Dist = 0;
        public Int32  CostNum;
        public ESkillCostType CostType;
        public List<ActData>  Actions = new List<ActData>();
        public ESkillPos      Pos = ESkillPos.Skill_0;

        public override void Read(XmlElement os)
        {
            for (int i = 0; i < os.Attributes.Count; i++)
            {
                XmlAttribute child = os.Attributes[i] as XmlAttribute;
                if (child == null) continue;
                switch (child.Name)
                {
                    case "Id":
                        this.Id = ReadInt32(child.Value);
                        break;
                    case "Name":
                        this.Name = child.Value;
                        break;
                    case "Dur":
                        this.Dur = ReadFloat(child.Value);
                        break;
                    case "Speed":
                        this.Speed = ReadFloat(child.Value);
                        break;
                    case "CD":
                        this.CD = ReadFloat(child.Value);
                        break;
                    case "Dist":
                        this.Dist = ReadFloat(child.Value);
                        break;
                    case "CostType":
                        this.CostType = (ESkillCostType)ReadInt32(child.Value);
                        break;
                    case "CostNum":
                        this.CostNum = ReadInt32(child.Value);
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
            Write(doc, os, "Id", Id);
            Write(doc, os, "Name", Name);
            Write(doc, os, "Dur", Dur);
            Write(doc, os, "Speed", Speed);
            Write(doc, os, "CD", CD);
            Write(doc, os, "Dist", Dist);
            Write(doc, os, "CostType", (int)CostType);
            Write(doc, os, "CostNum", CostNum);
            for (int i = 0; i < Actions.Count; i++)
            {
                Actions[i].Write(doc, os);
            }
        }
    }
}

