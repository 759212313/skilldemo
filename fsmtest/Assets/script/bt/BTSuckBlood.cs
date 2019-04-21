using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

namespace BT
{
    public class SuckBlood:BTTask
    {
        public float Percent = 1;
        public float Rate    = 0.1f;

        public override void Load(XmlElement xe)
        {
            base.Load(xe);
            Rate = EXml.ReadFloat(xe, "Rate");
            Percent = EXml.ReadFloat(xe, "Percent");
        }

        protected override bool Enter()
        {
            base.Enter();
            //List<Actor> list = (List<Actor>)BTTreeManager.Instance.GetData(this, Define.BT_JUDGE_LIST);
            //if (list == null)
            //{
            //    return false;
            //}
            //for (int i = 0; i < list.Count; i++)
            //{
            //    Actor actor = list[i];
            //    int dmg = actor.GetCurrAttr().GetAttr(EAttr.Atk);
            //    dmg = (int)(dmg * Percent);
            //    Owner.SuckBlood(actor, dmg,Rate);

            //}
            return true;
        }

        public override BTNode DeepClone()
        {
            SuckBlood pNode = new SuckBlood();
            pNode.Rate = Rate;
            pNode.Percent = Percent;
            return pNode;
        }
    }
}
