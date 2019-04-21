using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class DBEffect:DBModule
{
    public int Id;
    public string Path;

    public override int GetTypeId()
    {
        return Id;
    }
}

public sealed class ReadDBEffect : IReadConfig<int, DBEffect>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBEffect> dict)
    {
        DBEffect db = new DBEffect();
        db.Id = query.GetInt("Id");
        db.Path = query.GetString("Path");
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}