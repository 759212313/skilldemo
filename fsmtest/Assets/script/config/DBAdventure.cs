using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBAdventure : DBModule
{
    public int Id;
    public string Name = string.Empty;
    public string Icon = string.Empty;
    public int Times;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBAdventure : IReadConfig<int, DBAdventure>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBAdventure> dict)
    {
        DBAdventure db = new DBAdventure();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Icon = query.GetString("Icon");
        db.Times = query.GetInt("Times");
        dict.Add(db.Id, db);
    }
}
