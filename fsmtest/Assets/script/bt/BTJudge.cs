using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

namespace BT
{
    public class Judge : BTTask
    {
        public ERegionType Region = ERegionType.None;
        public EAffect Affect = EAffect.None;
        public float LifeTime;
        public int   MaxCount;
        public string JudgeName = string.Empty;
        public BTScopeShape Scope;

        protected Transform mJudgeTrans;
        protected int       mCurCount = 0;

        protected override bool Enter()
        {
            base.Enter();
            if(Scope==null)
            {
                return false;
            }
            if(string.IsNullOrEmpty(JudgeName))
            {
                //Debug.LogError(Owner.Id+"判断Transform Key为空");
                return false;
            }
            return FindJudgeTrans();
        }

        private bool FindJudgeTrans()
        {
            mJudgeTrans = BTTreeManager.Instance.GetData(this, JudgeName) as Transform;
            if (mJudgeTrans == null)
            {
                //Debug.LogError(Owner.Id + "找不到参照物");
                return false;
            }
            Scope.Center = mJudgeTrans;
            return true;
        }

        private bool CheckHitLimit()
        {
            return MaxCount > 0 && mCurCount >= MaxCount;
        }

        protected override EBTStatus Execute()
        {
            if (FindJudgeTrans()==false)
            {
                return EBTStatus.BT_FAILURE;
            }
            List<Actor> list = Owner.GetActorsByAffectType(Affect);
            List<Actor> hits = new List<Actor>();
            for (int i = 0; i < list.Count; i++)
            {
                Actor actor = list[i];
                if (Scope.IsTouch(actor))
                {
                    mCurCount++;
                    hits.Add(actor);
                }
                if (CheckHitLimit())
                {
                    break;
                }
            }

            if (hits.Count > 0)
            {
                BTTreeManager.Instance.SaveData(this, Define.BT_JUDGE_LIST, hits);
                BTTreeManager.Instance.SaveData(this, Define.BT_JUDGE_SUCCESS, string.Empty);
                if (mChildren.Count > 0)
                {
                    BTNode pNode = mChildren[0];
                    pNode.Step();
                }
            }

            if (LifeTime>0)
            {
                return CheckHitLimit() ? EBTStatus.BT_SUCCESS : EBTStatus.BT_RUNNING;
            }
            else
            {
                return mCurCount > 0 ? EBTStatus.BT_SUCCESS : EBTStatus.BT_FAILURE;
            }
        }

        public override void Load(XmlElement xe)
        {
            base.Load(xe);
            switch (this.Region)
            {
                case ERegionType.Sphere:
                    {
                        Scope = new ScopeSphere();
                        ScopeSphere v = Scope as ScopeSphere;
                        v.Offset = EXml.ReadVector3(xe, "Offset");
                        v.Euler = EXml.ReadVector3(xe, "Euler");
                        v.Radius = EXml.ReadFloat(xe, "Radius");
                    }
                    break;
                case ERegionType.Box:
                    {
                        Scope = new ScopeBox();
                        ScopeBox v = Scope as ScopeBox;
                        v.Offset = EXml.ReadVector3(xe, "Offset");
                        v.Euler = EXml.ReadVector3(xe, "Euler");
                        v.H = EXml.ReadFloat(xe, "H");
                        v.L = EXml.ReadFloat(xe, "L");
                        v.W = EXml.ReadFloat(xe, "W");
                    }
                    break;
                case ERegionType.Cylinder:
                    {
                        Scope = new ScopeCylinder();
                        ScopeCylinder v = Scope as ScopeCylinder;
                        v.Offset = EXml.ReadVector3(xe, "Offset");
                        v.Euler = EXml.ReadVector3(xe, "Euler");
                        v.MaxDis = EXml.ReadFloat(xe, "MaxDis");
                        v.HAngle = EXml.ReadFloat(xe, "HAngle");
                        v.Height = EXml.ReadFloat(xe, "Height");
                    }
                    break;
            }
        }

        protected override void ReadAttribute(string key, string value)
        {
            switch (key)
            {
                case "Region":
                    this.Region = (ERegionType)value.ToInt32();
                    break;
                case "Affect":
                    this.Affect = (EAffect)value.ToInt32();
                    break;
                case "LifeTime":
                    this.LifeTime = value.ToFloat();
                    break;
                case "MaxCount":
                    this.MaxCount = value.ToInt32();
                    break;
                case "JudgeName":
                    this.JudgeName = value;
                    break;
            }
        }

        public override BTNode DeepClone()
        {
            Judge scope = new Judge();
            scope.Region = this.Region;
            scope.Scope = this.Scope;
            scope.Owner = this.Owner;
            scope.Affect = this.Affect;
            scope.LifeTime = this.LifeTime;
            scope.MaxCount = this.MaxCount;
            scope.JudgeName = this.JudgeName;
            scope.CloneChildren(this);
            return scope;
        }

        public override void Clear()
        {
            base.Clear();
            mJudgeTrans = null;
            mCurCount = 0;
        }
    }
}
