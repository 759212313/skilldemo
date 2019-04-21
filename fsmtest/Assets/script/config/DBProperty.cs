using System;
using System.Collections.Generic;
using System.Text;

public class DBProperty:DBModule
{
    public EProperty Property;
    public string Name;
    public int Factor;
    public bool IsPercent;
    public string Desc;

    public override int GetTypeId()
    {
        return (int)Property;
    }
}

public class ReadDBProperty:IReadConfig<EProperty,DBProperty>
{
    protected override void LoadData(SQLiteQuery query,Dictionary<EProperty,DBProperty> dict)
    {
        DBProperty db = new DBProperty();
        db.Property = (EProperty)query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Factor = query.GetInt("Factor");
        db.Desc = query.GetString("Desc");
        db.IsPercent = query.GetBool("IsPercent");
        if (!dict.ContainsKey(db.Property))
        {
            dict.Add(db.Property, db);
        }
    }
}