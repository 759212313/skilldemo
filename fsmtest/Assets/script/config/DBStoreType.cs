using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBStoreType:DBModule
{
    public int Id;
    public string Name;
    public bool Random;
    public string Icon1;
    public string Icon2;
    public int Limit;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBStoreType : IReadConfig<int, DBStoreType>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBStoreType> dict)
    {
        DBStoreType db = new DBStoreType();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Icon1 = query.GetString("Icon1");
        db.Icon2 = query.GetString("Icon2");
        db.Random = query.GetInt("Random") == 1;
        db.Limit = query.GetInt("Limit");
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}