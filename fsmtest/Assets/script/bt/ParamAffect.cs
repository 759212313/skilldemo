using UnityEngine;
using System.Collections;
using System;

namespace Cfg.Act
{
    public class ParamAffect : ParamBase
    {
        public EAffect Type = EAffect.Self;
        public ESelectTargetPolicy Policy = ESelectTargetPolicy.Normal;
        public Int32 MaxNum = 1;

        public override void Read(string[] array)
        {
            this.Type = (EAffect)CfgObj.ReadInt32(Get(0, array));
            this.Policy = (ESelectTargetPolicy)CfgObj.ReadInt32(Get(1, array));
            this.MaxNum = CfgObj.ReadInt32(Get(2, array));
        }

        public override void Save(ref string s)
        {
            Set(0, (int)Type, ref s);
            Set(1, (int)Policy, ref s);
            Set(2, MaxNum, ref s);
        }
    }
}

