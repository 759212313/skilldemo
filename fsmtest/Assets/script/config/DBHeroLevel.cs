using System;
using System.Collections.Generic;
using System.Text;

public class DBHeroLevel:DBModule
{
    public int Level;
    public int RequireExp;
    public int NextLevel;
    public Dictionary<EProperty, int> Propertys;

    public DBHeroLevel()
    {
        Propertys = new Dictionary<EProperty, int>();
    }

    public void AddProperty(EProperty property,int value)
    {
        if (value >= 0)
        {
            Propertys.Add(property, value);
        }
    }

    public override int GetTypeId()
    {
        return Level;
    }
}

public class ReadDBHeroLevel:IReadConfig<int,DBHeroLevel>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int,DBHeroLevel> dict)
    {
        DBHeroLevel db = new DBHeroLevel();
        db.Level = query.GetInt("Level");
        db.RequireExp = query.GetInt("RequireExp");
        db.NextLevel = query.GetInt("NextLevel");

        for (int i = 1; i <= 10; i++)
        {
            int value = query.GetInt("P" + i);
            db.AddProperty((EProperty)i, value);
        }

        if (!dict.ContainsKey(db.Level))
        {
            dict.Add(db.Level, db);
        }
    }
}
