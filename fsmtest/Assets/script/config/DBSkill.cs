using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBSkill : DBModule
{
    public int Id;
    public string Name = string.Empty;
    public bool IsNormal = false;
    public bool IsDisplay = false;
    public string Icon = string.Empty;
    public ESkillType Type;
    public int EntinyId;
    public int Priority;
    public int CountDown;
    public ESkillCostType CostType;
    public int CostNum;
    public string Desc = string.Empty;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBSkill : IReadConfig<int, DBSkill>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBSkill> dict)
    {
        DBSkill db = new DBSkill();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Icon = query.GetString("Icon");
        db.Type = (ESkillType)query.GetInt("SkillType");
        db.IsNormal = query.GetInt("IsNormal")==1;
        db.IsDisplay = query.GetInt("IsDisplay") == 1;
        db.EntinyId= query.GetInt("EntinyId");
        db.Priority = query.GetInt("Priority");
        db.CountDown = query.GetInt("CountDown");
        db.CostType = (ESkillCostType)query.GetInt("CostType");
        db.CostNum = query.GetInt("CostNum");
        db.Desc = query.GetString("Desc");
        dict.Add(db.Id, db);
    }
}

