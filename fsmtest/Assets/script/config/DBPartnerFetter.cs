using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBPartnerFetter:DBModule
{
    public int Id;
    public string Name = string.Empty;
    public int[] Targets = new int[2];
    public int PropertyID;
    public int PropertyNum;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBPartnerFetter : IReadConfig<int, DBPartnerFetter>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartnerFetter> dict)
    {
        DBPartnerFetter db = new DBPartnerFetter();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.PropertyID = query.GetInt("PropertyID");
        db.PropertyNum = query.GetInt("PropertyNum");
        for (int i = 1; i <= 2; i++)
        {
            db.Targets[i - 1] = query.GetInt("Target" + i);
        }
        if(dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}