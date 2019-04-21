using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum ESkillTalentType
{
    TYPE_NONE                    =0,
    TYPE_STRENG_SKILL            =1,
    TYPE_NEW_SKILL               =2,
    TYPE_NEW_AND_REPLACE_SKILL   =3
}

public class DBSkillTalent : DBModule
{
    public int Id;
    public string Name = string.Empty;
    public int Pos;
    public string Icon = string.Empty;
    public int Layer;
    public ECarrer Carrer;
    public ESkillTalentType Type;
    public int TargetSkillId;
    public string Desc = string.Empty;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBSkillTalent : IReadConfig<int, DBSkillTalent>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBSkillTalent> dict)
    {
        DBSkillTalent db = new DBSkillTalent();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Pos = query.GetInt("Pos");
        db.Icon = query.GetString("Icon");
        db.Layer = query.GetInt("Layer");
        db.Carrer = (ECarrer)query.GetInt("Carrer");
        db.Type = (ESkillTalentType)query.GetInt("TalentSkillType");
        db.TargetSkillId = query.GetInt("TargetSkillId");
        db.Desc = query.GetString("Desc");
        dict.Add(db.Id,db);
    }
}
