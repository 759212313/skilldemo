using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CGemProperty
{
    public EProperty Property;
    public int Value;
    public int UnLockLevel;

    public CGemProperty(EProperty e,int v,int l)
    {
        Property = e;
        Value = v;
        UnLockLevel = l;
    }
}

public class DBGem:DBModule
{
    public int Id;
    public string Name;
    public int Quality;
    public List<CGemProperty> Propertys = new List<CGemProperty>();
    public int Suit;
    public int Pos;
    public int Exp;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBGem : IReadConfig<int, DBGem>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBGem> dict)
    {
        DBGem db = new DBGem();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Quality = query.GetInt("Quality");
        db.Suit = query.GetInt("Suit");
        db.Exp = query.GetInt("Exp");
        db.Pos = query.GetInt("Pos");

        for (int i = 1; i <= 3; i++)
        {
            EProperty k = (EProperty)query.GetInt("PropertyId" + i);
            int v = query.GetInt("PropertyNum" + i);
            int l = query.GetInt("UnLockLevel" + i);
            CGemProperty gem = new CGemProperty(k, v, l);

            db.Propertys.Add(gem);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}