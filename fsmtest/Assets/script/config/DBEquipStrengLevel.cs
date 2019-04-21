using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBEquipStrengLevel:DBModule
{
    public int Quality;
    public int Level;
    public int RequireExp;

    public override int GetTypeId()
    {
        return Level;
    }
}


public class ReadDBEquipStrengLevel : IReadConfig<int, Dictionary<int, DBEquipStrengLevel>>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, Dictionary<int, DBEquipStrengLevel>> dict)
    {
        DBEquipStrengLevel db = new DBEquipStrengLevel();
        db.Quality = query.GetInt("Quality");
        db.Level = query.GetInt("Level");
        db.RequireExp = query.GetInt("RequireExp");

        Dictionary<int, DBEquipStrengLevel> d;
        if (dict.ContainsKey(db.Quality))
        {
            d = dict[db.Quality];
        }
        else
        {
            d = new Dictionary<int, DBEquipStrengLevel>();
            dict.Add(db.Quality, d);
        }

        if (!d.ContainsKey(db.Level))
        {
            d.Add(db.Level, db);
        }
    }
}