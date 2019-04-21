using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBPartnerStar : DBModule
{
    public int Id;
    public int[] CostItemIDArray = new int[2];
    public int[] CostItemNumArray = new int[2];
    public int AddProperty;
    public int RequireLevel;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBPartnerStar : IReadConfig<int, DBPartnerStar>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartnerStar> dict)
    {
        DBPartnerStar db = new DBPartnerStar();
        db.Id = query.GetInt("Id");
        db.AddProperty = query.GetInt("AddProperty");
        db.RequireLevel = query.GetInt("RequireLevel");
        for (int i = 1; i <= 2; i++)
        {
            db.CostItemIDArray[i - 1] = query.GetInt("CostItemID" + i);
            db.CostItemNumArray[i - 1] = query.GetInt("CostItemNum" + i);
        }
        if (dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}