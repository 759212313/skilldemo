using System;
using System.Collections.Generic;


public class DBItem:DBModule
{
    public int Id;
    public string Name;
    public int Quality;
    public EItemType ItemType;
    public EBagType BagType;
    public ECarrer Carrer;
    public string Model_R;
    public string Model_L;
    public string Icon;
    public int SellMoneyId;
    public int SellMoneyNum;
    public string Desc;
    public int Data1;
    public int Data2;

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBItem:IReadConfig<int,DBItem>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int,DBItem> dict)
    {
        DBItem db = new DBItem();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.ItemType = (EItemType)query.GetInt("ItemType");
        db.BagType = (EBagType)query.GetInt("BagType");
        db.Quality = query.GetInt("Quality");
        db.Icon = query.GetString("Icon");
        db.SellMoneyId = query.GetInt("SellMoneyId");
        db.SellMoneyNum = query.GetInt("SellMoneyNum");
        db.Desc = query.GetString("Desc");
        db.Data1 = query.GetInt("Data1");
        db.Data2 = query.GetInt("Data2");
        db.Model_R = query.GetString("Model_R");
        db.Model_L = query.GetString("Model_L");
        db.Carrer = (ECarrer)query.GetInt("Carrer");

        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}