using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CEquipStreng
{
    public EProperty Property;
    public int Value;
    public int UnlockLevel;
}

public class DBEquipStreng:DBModule
{
    public int Id;
    public List<CEquipStreng> Propertys = new List<CEquipStreng>();

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBEquipStreng : IReadConfig<int,DBEquipStreng>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int,DBEquipStreng> dict)
    {
        DBEquipStreng db = new DBEquipStreng();
        db.Id = query.GetInt("Id");

        for (int i = 1; i <= 6; i++)
        {
            CEquipStreng data = new CEquipStreng();
            data.Property = (EProperty)query.GetInt("PropertyId" + i);
            data.Value = query.GetInt("PropertyNum" + i);
            data.UnlockLevel = query.GetInt("PropertyLevel" + i);
            db.Propertys.Add(data);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}