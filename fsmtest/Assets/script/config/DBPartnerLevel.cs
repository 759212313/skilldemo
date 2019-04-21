using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBPartnerLevel : DBModule
{
    public int Id;
    public int Partner;
    public int Level;
    public int Exp;
    public int[] Propertys = new int[3];

    public override int GetTypeId()
    {
        return Level;
    }
}

public class ReadDBPartnerLevel : IReadConfig<int, DBPartnerLevel>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartnerLevel> dict)
    {
        DBPartnerLevel db = new DBPartnerLevel();
        db.Id = query.GetInt("Id");
        db.Level = query.GetInt("Level");
        db.Partner= query.GetInt("Partner");
        db.Exp = query.GetInt("Exp");
        for (int i = 1; i <= 3; i++)
        {
            db.Propertys[i - 1] = query.GetInt("Property" + i);
        }
        if (!dict.ContainsKey(db.Level))
        {
            dict[db.Id] = db;
        }
    }
}