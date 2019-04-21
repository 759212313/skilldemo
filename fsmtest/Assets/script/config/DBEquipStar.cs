using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBEquipStar:DBModule
{
    public int Quality;
    public int StarLevel;
    public int Percent;
    public int CostItemId;
    public int CostMoneyId;
    public int CostMoneyNum;
    public int CostItemNum;

    public override int GetTypeId()
    {
        return StarLevel;
    }
}

public class ReadDBEquipStar : IReadConfig<int, Dictionary<int,DBEquipStar>>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, Dictionary<int, DBEquipStar>> dict)
    {
        DBEquipStar db = new DBEquipStar();
        db.Quality = query.GetInt("Quality");
        db.StarLevel = query.GetInt("StarLevel");
        db.Percent = query.GetInt("Percent");
        db.CostItemId = query.GetInt("CostItemId");
        db.CostMoneyId = query.GetInt("CostMoneyId");
        db.CostItemNum = query.GetInt("CostItemNum");
        db.CostMoneyNum = query.GetInt("CostMoneyNum");

        Dictionary<int, DBEquipStar> dt = null;
        if (dict.ContainsKey(db.Quality))
        {
            dt = dict[db.Quality];
        }
        else
        {
            dt = new Dictionary<int, DBEquipStar>();
            dict.Add(db.Quality, dt);
        }
        if (!dt.ContainsKey(db.StarLevel))
        {
            dt.Add(db.StarLevel, db);
        }
    }

}