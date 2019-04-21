using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBEntiny : DBModule
{
    public int Id;
    public int Level;
    public string Name= string.Empty;
    public string Ctrl= string.Empty;
    public string Model= string.Empty;
    public string Desc = string.Empty;
    public string Title = string.Empty;
    public string Icon = string.Empty;
    public EActorRace Race;
    public EActorSex  Sex;
    public EActorType Type;
    public EMonsterType MonsterType;
    public int Group;
    public int Quality;
    public int Exp;
    public float WSpeed;
    public float RSpeed;
    public int BornEffectID;
    public int DeadEffectID;
    public Vector3 StagePos;
    public float StageScale;
    public string Voice = string.Empty;
    public string AIFeature = string.Empty;
    public string AIScript = string.Empty;
    public string SkillScript = string.Empty;
    public Dictionary<EProperty, int> Propertys = new Dictionary<EProperty, int>();

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBEntiny : IReadConfig<int, DBEntiny>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBEntiny> dict)
    {
        DBEntiny db = new DBEntiny();
        db.Id = query.GetInt("Id");
        db.Level = query.GetInt("Level");
        db.Name = query.GetString("Name");
        db.Model = query.GetString("Model");
        db.Desc = query.GetString("Desc");
        db.Ctrl = query.GetString("Ctrl");
        db.Icon = query.GetString("Icon");
        db.Title = query.GetString("Title");

        db.Race = (EActorRace)query.GetInt("Race");
        db.Type = (EActorType)query.GetInt("Type");
        db.Sex = (EActorSex)query.GetInt("Sex");
        db.MonsterType = (EMonsterType)query.GetInt("MonsterType");
        db.Group = query.GetInt("Group");
        db.Quality = query.GetInt("Quality");

        db.RSpeed = query.GetFloat("RSpeed");
        db.WSpeed = query.GetFloat("WSpeed");
        db.BornEffectID = query.GetInt("BornEffectID");
        db.DeadEffectID = query.GetInt("DeadEffectID");
        db.StagePos = query.GetString("StagePos").ToVector3(true);
        db.StageScale = query.GetFloat("StageScale");

        db.Voice = query.GetString("Voice");
        db.AIFeature = query.GetString("AIFeature");
        db.AIScript = query.GetString("AIScript");
        db.SkillScript = query.GetString("SkillScript");
        db.Exp = query.GetInt("Exp");

        for (int i = 1; i <= 10; i++)
        {
            int v = query.GetInt("P" + i);
            db.Propertys.Add((EProperty)i, v);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}