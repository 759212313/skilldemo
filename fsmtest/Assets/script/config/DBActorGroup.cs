using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBActorGroup:DBModule
{
    public int Id;
    public string Name;
    public int ChiefID;
    public EActorType ChiefActorType;

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBActorGroup : IReadConfig<int, DBActorGroup>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBActorGroup> dict)
    {
        DBActorGroup db = new DBActorGroup();
        db.Id = query.GetInt("Id");
        db.ChiefID = query.GetInt("ChiefID");
        db.Name = query.GetString("Name");
        db.ChiefActorType = (EActorType)query.GetInt("ChiefActorType");
        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}