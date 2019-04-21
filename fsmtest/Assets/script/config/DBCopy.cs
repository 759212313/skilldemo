using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum ECopyType
{
    TYPE_WORLD = 0,
    TYPE_EASY = 1,
    TYPE_ELITE,
    TYPE_EPIC,
    TYPE_DAILY,
    TYPE_ARENA
}


public enum EStarCondition
{
    TYPE_PASSCOPY,
    TYPE_MAIN_HEALTH,
    TYPE_TIME_LIMIT,
}

public class DBCopy:DBModule
{
    public int Id;
    public ECopyType CopyType;
    public string Name;
    public int CostActionId;
    public int CostActionNum;
    public int AwardId;
    public int FirstAwardId;
    public int SceneId;
    public int GetMoneyId;
    public int GetMoneyRatio;
    public int GetExpRatio;
    public int BattleTimes;
    public int UnlockLevel;
    public string FightValue;
    public string Desc;
    public string Texture;
    public string Icon;
    public int DropBoxAwardId;

    public EStarCondition[] StarConditions = new EStarCondition[3];
    public int[]            StarValues     = new int[3];

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBCopy : IReadConfig<int, DBCopy>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBCopy> dict)
    {
        DBCopy db = new DBCopy();
        db.Id = query.GetInt("Id");
        db.CopyType = (ECopyType)query.GetInt("CopyType");
        db.Name = query.GetString("Name");
        db.CostActionId = query.GetInt("CostActionId");
        db.CostActionNum = query.GetInt("CostActionNum");
        db.AwardId = query.GetInt("AwardId");
        db.FirstAwardId = query.GetInt("FirstAwardId");
        db.DropBoxAwardId = query.GetInt("DropBoxAwardId");
        db.SceneId = query.GetInt("SceneId");
        db.GetMoneyId = query.GetInt("GetMoneyId");
        db.GetMoneyRatio = query.GetInt("GetMoneyRatio");
        db.GetExpRatio = query.GetInt("GetExpRatio");
        db.BattleTimes = query.GetInt("BattleTimes");
        db.UnlockLevel = query.GetInt("UnlockLevel");
        db.FightValue = query.GetString("FightValue");
        db.Desc = query.GetString("Desc");
        db.Texture = query.GetString("Texture");
        db.Icon = query.GetString("Icon");

        for(int i=1;i<=3;i++)
        {
            db.StarValues[i-1]= query.GetInt("StarValue" + i);
            db.StarConditions[i-1]= (EStarCondition)query.GetInt("StarCondition" + i);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}
