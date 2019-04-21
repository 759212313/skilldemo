using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DBGoto:DBModule
{
    public int Id;
    public string Desc;
    public string Icon;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBGoto : IReadConfig<int, DBGoto>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBGoto> dict)
    {

    }
}
