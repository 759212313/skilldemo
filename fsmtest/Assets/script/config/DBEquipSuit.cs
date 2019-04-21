using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBEquipSuit:DBModule
{
    public int Id;
    public string Name;
    public List<Dictionary<EProperty, int>> SuitPropertys = new List<Dictionary<EProperty, int>>();

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBEquipSuit : IReadConfig<int, DBEquipSuit>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBEquipSuit> dict)
    {
        DBEquipSuit db = new DBEquipSuit();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");

        for (int i = 1; i <= 3; i++)
        {
            string[] suit = query.GetString("Suit" + i).Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string[] idArray = query.GetString("SuitPropertyId" + i).Split(new char[1] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<EProperty, int> d = new Dictionary<EProperty, int>();
            for (int j = 0; j < suit.Length; j++)
            {
                EProperty e = (EProperty)idArray[j].ToInt32();
                int v = suit[j].ToInt32();
                if (!d.ContainsKey(e))
                {
                    d.Add(e, v);
                }
            }
            if (!db.SuitPropertys.Contains(d))
            {
                db.SuitPropertys.Add(d);
            }
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}