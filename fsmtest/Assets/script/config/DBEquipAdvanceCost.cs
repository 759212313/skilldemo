using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBEquipAdvanceCost:DBModule
{
    public int Quality;
    public int AdvanceLevel;
    public int CostItemId;
    public int CostItemNum;
    public int CostMoneyNum;
    public int CostMoneyId;
    public int CostEquipNum;

    public override int GetTypeId()
    {
        return AdvanceLevel;
    }
}


public class ReadDBEquipAdvanceCost : IReadConfig<int, Dictionary<int, DBEquipAdvanceCost>>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, Dictionary<int, DBEquipAdvanceCost>> dict)
    {
        DBEquipAdvanceCost db = new DBEquipAdvanceCost();
        db.Quality = query.GetInt("Quality");
        db.AdvanceLevel = query.GetInt("AdvanceLevel");
        db.CostItemId = query.GetInt("CostItemId");
        db.CostItemNum = query.GetInt("CostItemNum");
        db.CostMoneyId = query.GetInt("CostMoneyId");
        db.CostMoneyNum = query.GetInt("CostMoneyNum");
        db.CostEquipNum = query.GetInt("CostEquipNum");

        Dictionary<int, DBEquipAdvanceCost> d;
        if (dict.ContainsKey(db.Quality))
        {
            d = dict[db.Quality];
        }
        else
        {
            d = new Dictionary<int, DBEquipAdvanceCost>();
            dict.Add(db.Quality, d);
        }

        if (!d.ContainsKey(db.AdvanceLevel))
        {
            d.Add(db.AdvanceLevel, db);
        }
    }
}