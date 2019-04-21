using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DBGemSuit:DBModule
{
    public int Id;
    public string Name;
    public List<Dictionary<EProperty, int>> SuitPropertys = new List<Dictionary<EProperty, int>>();
    public string SuitDesc;

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBGemSuit : IReadConfig<int, DBGemSuit>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBGemSuit> dict)
    {
        DBGemSuit db = new DBGemSuit();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.SuitDesc = query.GetString("SuitDesc");
        for (int i = 1; i <= 3; i++)
        {
            string[] suit = query.GetString("Suit" + i).Split('|');
            Dictionary<EProperty, int> propertys = new Dictionary<EProperty, int>();
            propertys.Add(EProperty.LHP, suit[0].ToInt32());
            propertys.Add(EProperty.ATK, suit[1].ToInt32());
            db.SuitPropertys.Add(propertys);
        }

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}