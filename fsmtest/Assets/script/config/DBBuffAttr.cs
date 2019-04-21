using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CBuffAttr
{
    public EAttr Attr;
    public EValueType ValueType;
    public Int32 Value;
}

public class DBBuffAttr:DBModule
{
    public int Id;
    public string Name;
    public List<CBuffAttr> Attrs = new List<CBuffAttr>();

    public override int GetTypeId()
    {
        return Id;
    }
}


public class ReadDBBuffAttr : IReadConfig<int, DBBuffAttr>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBBuffAttr> dict)
    {
        DBBuffAttr db = new DBBuffAttr();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        for (int i = 1; i <= 3; i++)
        {
            int attr = query.GetInt("Attr" + i);
            if (attr > 0)
            {
                CBuffAttr data = new CBuffAttr();
                data.Attr = (EAttr)attr;
                data.Value = query.GetInt("Value" + i);
                data.ValueType = (EValueType)query.GetInt("ValueType" + i);
                db.Attrs.Add(data);
            }
        }
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}