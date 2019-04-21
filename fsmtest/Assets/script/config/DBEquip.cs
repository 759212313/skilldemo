using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DBEquip:DBModule
{
    public int Id;
    public string Name;
    public int Quality;
    public int Pos;
    public Dictionary<EProperty, int> Propertys = new Dictionary<EProperty, int>();
    public int StrengthGrow1;
    public int StrengthGrow2;
    public int StrengthGrow3;
    public int DeComposeNum1;
    public int DeComposeNum2;
    public int DeComposeId1;
    public int DeComposeId2;
    public int Suit;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBEquip : IReadConfig<int, DBEquip>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBEquip> dict)
    {
        DBEquip db = new DBEquip();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Quality = query.GetInt("Quality");
        db.Pos = query.GetInt("Pos");

        for (int i = 1; i <= 10; i++)
        {
            int p = query.GetInt("P" + i);
            db.Propertys.Add((EProperty)i, p);
        }

        db.Suit = query.GetInt("Suit");
        db.StrengthGrow1 = query.GetInt("StrengthenGrow1");
        db.StrengthGrow2 = query.GetInt("StrengthenGrow2");
        db.StrengthGrow3 = query.GetInt("StrengthenGrow3");
        db.DeComposeId1 = query.GetInt("DeComposeId1");
        db.DeComposeId2 = query.GetInt("DeComposeId2");
        db.DeComposeNum1 = query.GetInt("DeComposeNum1");
        db.DeComposeNum2 = query.GetInt("DeComposeNum2");

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}