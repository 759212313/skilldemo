using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DBVIP: DBModule
{
    public int Level;
    public int RequireExp;
    public int Desc;
    public string Icon;
    public int AwardID;

    public override int GetTypeId()
    {
        return Level;
    }
}
