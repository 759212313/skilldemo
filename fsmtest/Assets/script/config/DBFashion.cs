using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class DBFashion:DBModule
{
    public int Id;
    public List<KeyValuePair<EProperty, int>> Propertys = new List<KeyValuePair<EProperty, int>>();

    public override int GetTypeId()
    {
        return Id;
    }
}

public class DataReadFashion : IReadConfig<int, DBFashion>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBFashion> dict)
    {
        DBFashion db = new DBFashion();
        db.Id = query.GetInt("Id");
        for (int i = 1; i <= 2; i++)
        {
            EProperty e = (EProperty)query.GetInt("PropertyId" + i);
            int v = query.GetInt("PropertyNum" + i);
            KeyValuePair<EProperty, int> fp = new KeyValuePair<EProperty, int>(e, v);
            db.Propertys.Add(fp);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}
