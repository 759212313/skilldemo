using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBEquipAdvance:DBModule
{
    public int Id;
    public string Name;
    public int Quality;
    public List<KeyValuePair<EProperty, int>> Propertys = new List<KeyValuePair<EProperty, int>>();

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBEquipAdvance : IReadConfig<int, DBEquipAdvance>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBEquipAdvance> dict)
    {
        DBEquipAdvance db = new DBEquipAdvance();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Quality = query.GetInt("Quality");

        for (int i = 1; i <= 8; i++)
        {
            EProperty key = (EProperty)query.GetInt("PropertyId" + i);
            int value = query.GetInt("PropertyNum" + i);
            KeyValuePair<EProperty, int> e = new KeyValuePair<EProperty, int>(key, value);
            db.Propertys.Add(e);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}