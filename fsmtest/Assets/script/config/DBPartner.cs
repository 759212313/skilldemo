using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBPartner: DBModule
{
    public int Id;
    public int SoulItemID;
    public int[] Fetters = new int[6];
    public int[] Skills  = new int[4];

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBPartner : IReadConfig<int, DBPartner>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartner> dict)
    {
        DBPartner db = new DBPartner();
        db.Id = query.GetInt("Id");
        db.SoulItemID = query.GetInt("SoulItemID");
        for (int i = 1; i <= 6; i++)
        {
            db.Fetters[i - 1] = query.GetInt("Fetter" + i);
        }
        for (int i = 1; i <= 4; i++)
        {
            db.Skills[i - 1] = query.GetInt("Skill" + i);
        }
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}