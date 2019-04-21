using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT
{
    public class AddBuff : BTTask
    {
        public EAffect Affect;
        public int BuffID;
        public float Ratio = 1;

        protected override bool Enter()
        {
            base.Enter();
            //bool isTrigger = GTTools.IsTrigger(Ratio);
            //if(isTrigger==false)
            //{
            //    return false;
            //}
            //List<Actor> list = Owner.GetActorsByAffectType(Affect);
            //if (list == null)
            //{
            //    return false;
            //}

            //for(int i=0;i<list.Count;i++)
            //{
            //    list[i].GetActorBuff().AddBuff(BuffID,Owner);
            //}
            return true;
        }

        protected override void ReadAttribute(string key, string value)
        {
            switch (key)
            {
                case "Affect":
                    this.Affect = (EAffect)value.ToInt32();
                    break;
                case "BuffID":
                    this.BuffID = value.ToInt32();
                    break;
                case "Ratio":
                    this.Ratio = value.ToFloat();
                    break;
            }
        }

        protected override EBTStatus Execute()
        {
            return EBTStatus.BT_SUCCESS;
        }

        public override BTNode DeepClone()
        {
            AddBuff buff = new AddBuff();
            buff.Affect = this.Affect;
            buff.BuffID = this.BuffID;
            buff.Ratio = this.Ratio;
            return buff;
        }
    }
}