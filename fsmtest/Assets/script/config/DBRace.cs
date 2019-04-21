using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBRace:DBModule
{
    public int Id;
    public EActorRace Race;
    public string Name;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBRace : IReadConfig<int, DBRace>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBRace> dict)
    {
        DBRace db = new DBRace();
        db.Id = query.GetInt("Id");
        db.Race = (EActorRace)db.Id;
        db.Name = query.GetString("Name");
        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}