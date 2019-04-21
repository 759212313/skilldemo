using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBStore:DBModule
{
    public int Id;
    public int ItemID;
    public int ItemNum;
    public int CostMoneyID;
    public int CostMoneyNum;
    public int StoreType;

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBStore : IReadConfig<int, Dictionary<int,DBStore>>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, Dictionary<int, DBStore>> dict)
    {
        DBStore db = new DBStore();
        db.Id = query.GetInt("Id");
        db.ItemID = query.GetInt("ItemID");
        db.ItemNum = query.GetInt("ItemNum");
        db.CostMoneyNum = query.GetInt("CostMoneyNum");
        db.CostMoneyID = query.GetInt("CostMoneyID");
        db.StoreType = query.GetInt("StoreType");

        Dictionary<int, DBStore> d;
        if (dict.ContainsKey(db.StoreType))
        {
            d = dict[db.StoreType];
        }
        else
        {
            d = new Dictionary<int, DBStore>();
            dict.Add(db.StoreType, d);
        }

        if (!d.ContainsKey(db.Id))
        {
            d.Add(db.Id, db);
        }
    }


}