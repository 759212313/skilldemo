using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class DBMall:DBModule
{
    public int Id;
    public int BuyItemID;
    public int BuyItemNum;
    public EProduct ProductType;
    public int TimesLimit;
    public int CostMoneyID;
    public int CostMoneyNum;

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBMall : IReadConfig<int, DBMall>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBMall> dict)
    {
        DBMall db = new DBMall();
        db.Id = query.GetInt("Id");
        db.BuyItemID = query.GetInt("BuyItemID");
        db.BuyItemNum = query.GetInt("BuyItemNum");
        db.ProductType = (EProduct)query.GetInt("EProductType");
        db.TimesLimit = query.GetInt("TimesLimit");
        db.CostMoneyID = query.GetInt("CostMoneyID");
        db.CostMoneyNum = query.GetInt("CostMoneyNum");

        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}