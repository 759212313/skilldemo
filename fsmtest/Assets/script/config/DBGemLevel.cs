using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBGemLevel:DBModule
{
    public int Quality;
    public int Level;
    public int CostMoneyId;
    public int RequireExp;
    public int PropertyRatio;

    public override int GetTypeId()
    {
        return Level;
    }
}

public class ReadDBGemLevel : IReadConfig<int, Dictionary<int, DBGemLevel>>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, Dictionary<int, DBGemLevel>> dict)
    {
        DBGemLevel db = new DBGemLevel();
        db.Quality = query.GetInt("Quality");
        db.Level = query.GetInt("Level");
        db.CostMoneyId = query.GetInt("UpCostMoneyId");
        db.RequireExp = query.GetInt("RequireExp");
        db.PropertyRatio = query.GetInt("PropertyRatio");

        Dictionary<int, DBGemLevel> dt = null;
        if (dict.ContainsKey(db.Quality))
        {
            dt = dict[db.Quality];
        }
        else
        {
            dt = new Dictionary<int, DBGemLevel>();
            dict.Add(db.Quality, dt);
        }
        if (!dt.ContainsKey(db.Level))
        {
            dt.Add(db.Level, db);
        }
    }
}