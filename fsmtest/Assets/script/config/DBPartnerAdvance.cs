using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBPartnerAdvance:DBModule
{
    public int Id;
    public int Partner;
    public int Advance;
    public int CostSoulNum;
    public int MainTarget;
    public int ViceTarget;
    public Dictionary<EProperty, int> MainPropertys = new Dictionary<EProperty, int>();
    public Dictionary<EProperty, int> VicePropertys = new Dictionary<EProperty, int>();
    public string Desc1;
    public string Desc2;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBPartnerAdvance : IReadConfig<int, DBPartnerAdvance>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBPartnerAdvance> dict)
    {
        DBPartnerAdvance db = new DBPartnerAdvance();
        db.Id = query.GetInt("Id");
        db.Partner = query.GetInt("Partner");
        db.Advance = query.GetInt("Advance");
        db.CostSoulNum = query.GetInt("CostSoulNum");
        db.MainTarget = query.GetInt("MainTarget");
        db.ViceTarget = query.GetInt("ViceTarget");
        db.Desc1 = query.GetString("Desc1");
        db.Desc2 = query.GetString("Desc2");

        for (int i = 1; i <= 2; i++)
        {
            int mainID = query.GetInt("MainID" + i);
            int viceID = query.GetInt("ViceID" + i);
            if (mainID > 0)
            {
                EProperty id = (EProperty)mainID;
                int num = query.GetInt("MainNum" + i);
                db.MainPropertys.Add(id, num);

            }
            if (viceID > 0)
            {
                EProperty id = (EProperty)viceID;
                int num = query.GetInt("ViceNum" + i);
                db.VicePropertys.Add(id, num);
            }
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}