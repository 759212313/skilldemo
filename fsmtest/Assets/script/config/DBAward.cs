using System;
using System.Collections.Generic;

public enum EDropType
{
    ONERAN = 1,//随机掉落一定数量的单个物品
    MULFIX = 2,//固定掉落多个物品
    MULRAN = 3,//随机掉落多个物品
}

public enum ERecvType
{
    TYPE_RECV_ALL,//领取所有的
    TYPE_RECV_ONE,//几选一
}

public class DBAward:DBModule
{
    public int Id;
    public string Name;
    public EDropType DropType;
    public ERecvType RecvType;
    public string DropItems;
    public int MaxDropNum;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBAward : IReadConfig<int, DBAward>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBAward> dict)
    {
        DBAward db = new DBAward();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.DropType = (EDropType)query.GetInt("DropType");
        db.RecvType = (ERecvType)query.GetInt("RecvType");
        db.DropItems = query.GetString("DropItems");
        db.MaxDropNum = query.GetInt("MaxDropNum");
        if (!dict.ContainsKey(db.Id))
        {
            dict[db.Id] = db;
        }
    }
}
