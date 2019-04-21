using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using BT;

public class SkillTree : BTTree
{
    public float CD { get; private set; }
    public string Name { get; private set; }
    public ESkillPos Pos { get; private set; }
    public ESkillCostType CostType { get; private set; }
    public Int32 CostNum { get; private set; }
    public float StateTime { get; private set; }
    public float CastDistance { get; private set; }
    protected float mStartTimer;
    protected bool mFirstToUse = true;

    public SkillTree(int id, Actor owner)
    {
        this.Id = id;
        this.Owner = owner;
    }

    public override void Start()
    {
        mFirstToUse = false;
        mStartTimer = Time.realtimeSinceStartup;
        base.Start();  
    }

    public bool IsInCD()
    {
        return mFirstToUse==false&&Time.realtimeSinceStartup - mStartTimer < CD;
    }

    public float GetLeftTime()
    {
        float time = Time.realtimeSinceStartup - mStartTimer;
        if(time>CD||mFirstToUse==true)
        {
            return 0;
        }
        return CD - time;
    }

    protected override void ReadAttribute(string key, string value)
    {
        switch (key)
        {
            case "Name":
                this.Name = value;
                break;
            case "StateTime":
                this.StateTime = value.ToFloat();
                break;
            case "CD":
                this.CD = value.ToFloat();
                break;
            case "CostType":
                this.CostType = (ESkillCostType)value.ToInt32();
                break;
            case "CostNum":
                this.CostNum = value.ToInt32();
                break;
            case "Pos":
                this.Pos = (ESkillPos)value.ToInt32();
                break;
            case "CastDistance":
                this.CastDistance = value.ToFloat();
                break;
        }
    }

    protected override void SaveAttribute(XmlDocument doc, XmlElement xe)
    {
        xe.SetAttribute("Id", Id.ToString());
        xe.SetAttribute("Name", string.IsNullOrEmpty(Name) ? Id.ToString() : Name);
        xe.SetAttribute("StateTime", StateTime.ToString());
        xe.SetAttribute("CD", CD.ToString());
        xe.SetAttribute("CostType", ((int)CostType).ToString());
        xe.SetAttribute("CostNum", CostNum.ToString());
        xe.SetAttribute("Pos", Pos.ToString());
        xe.SetAttribute("CastDistance", CastDistance.ToString());
    }

    public override void Break()
    {
        //Owner.GetActorAction().Break();
        base.Break();
    }

    public override BTNode DeepClone()
    {
        SkillTree tree = new SkillTree(Id, Owner);
        tree.Id = this.Id;
        tree.CD = this.CD;
        tree.CostNum = this.CostNum;
        tree.CostType = this.CostType;
        tree.Name = this.Name;
        tree.Pos = this.Pos;
        tree.StateTime = this.StateTime;
        tree.CastDistance = this.CastDistance;
        tree.CloneChildren(this);
        return tree;
    }
}
