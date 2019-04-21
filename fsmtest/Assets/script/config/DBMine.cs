using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBMine : DBModule
{
    public int Id;
    public string Name = string.Empty;
    public string Model = string.Empty;
    public int CostItemID;
    public float CostTime;
    public float Radius;
    public int DropItemID;
    public bool IsShowMineBar;
    public bool IsShowName;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBMine : IReadConfig<int, DBMine>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBMine> dict)
    {
        DBMine db = new DBMine();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Model = query.GetString("Model");
        db.CostItemID = query.GetInt("CostItemID");
        db.CostTime = query.GetFloat("CostTime");
        db.Radius = query.GetFloat("Radius");
        db.DropItemID = query.GetInt("DropItemID");
        db.IsShowMineBar = query.GetBool("IsShowMineBar");
        db.IsShowName = query.GetBool("IsShowName");

        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}
