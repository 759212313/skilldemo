using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;


public class DBRune:DBModule
{
    public int Id;
    public EProperty PropertyId;
    public int PropertyNum;
    public ERuneType RuneType;
    public int ComposeCostMoneyId;
    public int ComposeCostMoneyNum;
    public int UpCostMoneyId;
    public List<KeyValuePair<int, int>> Composes = new List<KeyValuePair<int, int>>();
    public int[] UpValues = new int[5] { 0, 0, 0, 0, 0 };
    public int[] UpCostMoneys = new int[5] { 0, 0, 0, 0, 0 };

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBRune : IReadConfig<int, DBRune>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBRune> dict)
    {
        DBRune db = new DBRune();
        db.Id = query.GetInt("Id");
        db.PropertyId = (EProperty)query.GetInt("PropertyId");
        db.PropertyNum = query.GetInt("PropertyNum");
        db.RuneType = (ERuneType)query.GetInt("RuneType");
        db.ComposeCostMoneyId = query.GetInt("ComposeCostMoneyId");
        db.ComposeCostMoneyNum = query.GetInt("ComposeCostMoneyNum");
        db.UpCostMoneyId = query.GetInt("UpCostMoneyId");

        for (int i = 1; i <= 5; i++)
        {
            db.UpCostMoneys[i - 1] = query.GetInt("UpCostMoney" + i);
        }

        for (int i = 1; i <= 5; i++)
        {
            db.UpValues[i - 1] = query.GetInt("PropertyLevel" + i);
        }

        string compose = query.GetString("Compose");
        if (compose.Contains("|"))
        {
            Regex reg = new Regex(@"(?is)(?<=\()[^\)]+(?=\))");
            MatchCollection mc = reg.Matches(compose);
            for (int i = 0; i < mc.Count; i++)
            {
                string s = mc[i].Value;
                string[] array = s.Split('|');
                if (array.Length != 2)
                {
                    continue;
                }
                KeyValuePair<int, int> kv = new KeyValuePair<int, int>(array[1].ToInt32(), array[2].ToInt32());
                db.Composes.Add(kv);
            }
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}
