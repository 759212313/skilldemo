using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBPetLevel:DBModule
{
    public int Quality;
    public int Level;
    public int Exp;
    public int[] PropertyNums = new int[3];
    public int Ratio;

    public override int GetTypeId()
    {
        return Level;
    }
}

public class ReadDBPetLevel : IReadConfig<int, Dictionary<int,DBPetLevel>>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, Dictionary<int,DBPetLevel>> dict)
    {
        DBPetLevel db = new DBPetLevel();
        db.Quality = query.GetInt("Quality");
        db.Level = query.GetInt("Level");
        db.Exp = query.GetInt("Exp");
        db.Ratio = query.GetInt("Ratio");

        for (int i = 1; i <= 3; i++)
        {
            db.PropertyNums[i - 1] = query.GetInt("PropertyNum" + i);
        }

        Dictionary<int, DBPetLevel> d;
        if (dict.ContainsKey(db.Quality))
        {
            d = dict[db.Quality];
        }
        else
        {
            d = new Dictionary<int, DBPetLevel>();
            dict.Add(db.Quality, d);
        }

        if (!d.ContainsKey(db.Level))
        {
            d.Add(db.Level, db);
        }
    }
}