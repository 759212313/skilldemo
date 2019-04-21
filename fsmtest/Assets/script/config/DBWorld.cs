using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBWorld:DBModule
{
    public int Id;
    public string Name;
    public string Icon;
    public string Map;
    public ECopyType CopyType;
    public int[] Copys;
    public int[] Stars;
    public int[] Awards;
    public Vector2[] CopyPosArray;
    public string Desc;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBWorld : IReadConfig<int, DBWorld>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBWorld> dict)
    {
        DBWorld db = new DBWorld();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Icon = query.GetString("Icon");
        db.Map = query.GetString("Map");
        db.CopyType = (ECopyType)query.GetInt("Type");

        int stCopyId = query.GetInt("StCopyId");
        int edCopyId = query.GetInt("EdCopyId");
        int copyNum = edCopyId - stCopyId + 1;

        db.Copys = new int[copyNum];
        db.CopyPosArray = new Vector2[copyNum];
        for (int i = stCopyId; i <= edCopyId; i++)
        {
            int index = i - stCopyId;
            db.Copys[index] = i;
            string field = query.GetString("Pos" + (index + 1));
            db.CopyPosArray[index] = field.ToVector2();
        }

        db.Stars = new int[3];
        db.Awards = new int[3];
        for (int i = 0; i < 3; i++)
        {
            string s1 = "Star" + (i + 1);
            string s2 = "AwardId" + (i + 1);
            db.Stars[i] = query.GetInt(s1);
            db.Awards[i] = query.GetInt(s2);
        }
        db.Desc = query.GetString("Desc");

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}