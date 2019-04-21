using System;
using System.Collections.Generic;
using System.Text;

public class DBQuality:DBModule
{
    public int Quality;
    public string Name;
    public string Icon;
    public string Encode;
    public string Desc;

    public override int GetTypeId()
    {
        return Quality;
    }
}

public class ReadDBQuality:IReadConfig<int,DBQuality>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int,DBQuality> dict)
    {
        DBQuality db = new DBQuality();
        db.Quality = query.GetInt("Quality");
        db.Name = query.GetString("Name");
        db.Icon = query.GetString("Icon");
        db.Encode = query.GetString("Encode");
        db.Desc = query.GetString("Desc");
        if (!dict.ContainsKey(db.Quality))
        {
            dict.Add(db.Quality, db);
        }
    }
}