using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBCamera:DBModule
{
    public int Id;
    public ECameraType Type;
    public string Params;
    public float  DelayTime;
    public float  LastTime;
    public string Name;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBCamera : IReadConfig<int, DBCamera>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBCamera> dict)
    {
        DBCamera db = new DBCamera();
        db.Type = (ECameraType)query.GetInt("Type");
        db.Params = query.GetString("Params");
        db.LastTime = query.GetFloat("LastTime");
        db.DelayTime = query.GetFloat("DelayTime");
        db.Name = query.GetString("Name");
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}