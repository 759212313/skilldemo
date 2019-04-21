using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBBuff:DBModule
{
    public int Id;
    public string Name;
    public string Icon;
    public EBuffType BuffType;
    public EBuffOverlayType OverlayType;
    public EBuffDestroyType DestroyType;

    public float LifeTime;
    public int MaxOverlayNum;
    public int EffectID;
    public int Sort;

    public EEffectBind EffectBind;
    public Int32 ChangeModelID;
    public float ChangeModelScale;
    public string Desc;

    public EActType Result;
    public Int32 ResultAttr;
    public float ResultInterval;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBBuff : IReadConfig<int, DBBuff>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBBuff> dict)
    {
        DBBuff db = new DBBuff();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Icon = query.GetString("Icon");

        db.BuffType = (EBuffType)query.GetInt("BuffType");
        db.OverlayType = (EBuffOverlayType)query.GetInt("BuffOverType");
        db.DestroyType = (EBuffDestroyType)query.GetInt("DestroyType");

        db.LifeTime = query.GetFloat("LifeTime");
        db.MaxOverlayNum = query.GetInt("MaxOverlayNum");
        db.EffectID = query.GetInt("EffectID");
        db.Sort = query.GetInt("Sort");
        db.ChangeModelID = query.GetInt("ChangeModelID");
        db.ChangeModelScale = query.GetFloat("ChangeModelScale");
        db.Desc = query.GetString("Desc");
        db.EffectBind = (EEffectBind)query.GetInt("EffectBind");
        db.Result = (EActType)query.GetInt("Result");
        db.ResultAttr = query.GetInt("ResultAttrID");
        db.ResultInterval = query.GetFloat("ResultInterval");
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}