using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBPartnerWash : DBModule
{
    public int Level;
    public int[] PropertyMins = new int[3];
    public int[] PropertyMaxs = new int[3];

    public override int GetTypeId()
    {
        return Level;
    }
}

public class ReadDBPartnerWash : IReadConfig<int, DBPartnerWash>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartnerWash> dict)
    {
        DBPartnerWash db = new DBPartnerWash();
        db.Level = query.GetInt("Level");
        for (int i = 1; i <= 3; i++)
        {
            db.PropertyMins[i - 1] = query.GetInt("PropertyMin" + i);
            db.PropertyMaxs[i - 1] = query.GetInt("PropertyMax" + i);
        }
        if (dict.ContainsKey(db.Level))
        {
            dict.Add(db.Level, db);
        }
    }
}