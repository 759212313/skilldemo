using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBPartnerWake : DBModule
{
    public int Id;
    public int Level;
    public string Desc = string.Empty;
    public int CostSoulNum;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBPartnerWake : IReadConfig<int, DBPartnerWake>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartnerWake> dict)
    {
        DBPartnerWake db = new DBPartnerWake();
        db.Id = query.GetInt("Id");
        db.Level = query.GetInt("Level");
        db.CostSoulNum = query.GetInt("CostSoulNum");
        db.Desc = query.GetString("Desc");
        if (dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}