using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBRelics:DBModule
{
    public int Id;
    public string Name;
    public string Icon;
    public string Desc;
    public string Model;
    public int ActiveEffectID;
    public int DitiveEffectID;
    public int AddProperty;
    public int[] LevelRequireExp = new int[5];
    public int[] ArtificeCostID = new int[3];
    public EProperty[] PropertyID = new EProperty[3];
    public int[] PropertyNum = new int[3];
    public int SkillID;
    public Vector3 StagePos;
    public Vector3 StageEuler;
    public float   StageScale;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBRelics : IReadConfig<int, DBRelics>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBRelics> dict)
    {
        DBRelics db = new DBRelics();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Icon = query.GetString("Icon");
        for (int i = 1; i <= 3; i++)
        {
            EProperty p = (EProperty)query.GetInt("PropertyID" + i);
            int num = query.GetInt("PropertyNum" + i);
            db.PropertyID[i - 1] = p;
            db.PropertyNum[i - 1] = num;
        }

        db.Model = query.GetString("Model");
        db.ActiveEffectID = query.GetInt("ActiveEffectID");
        db.DitiveEffectID = query.GetInt("DitiveEffectID");

        for (int i = 1; i <= 5; i++)
        {
            int exp = query.GetInt("LevelExp" + i);
            db.LevelRequireExp[i - 1] = exp;
        }
        for (int i = 1; i <= 3; i++)
        {
            int id = query.GetInt("ArtificeCostID" + i);
            db.ArtificeCostID[i - 1] = id;
        }
        db.Desc = query.GetString("Desc");
        db.StagePos = query.GetString("StagePos").ToVector3(true);
        db.StageEuler = query.GetString("StageEuler").ToVector3(true);
        db.StageScale = query.GetFloat("StageScale");
        db.SkillID = query.GetInt("SkillID");
        db.AddProperty = query.GetInt("AddProperty");

        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}