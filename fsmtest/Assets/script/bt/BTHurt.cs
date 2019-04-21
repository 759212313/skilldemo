using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace BT
{
    public class Hurt : BTTask
    {
        public EDamageType Damage = EDamageType.NONE;
        public float Percent = 1;

        public override void Load(XmlElement xe)
        {
            base.Load(xe);
        }

        protected override void ReadAttribute(string key, string value)
        {
           switch(key)
            {
                case "Damage":
                    this.Damage = (EDamageType)value.ToInt32();
                    break;
                case "Percent":
                    this.Percent = value.ToFloat();
                    break;
            }
        }

        protected override bool Enter()
        {
            base.Enter();
            List<Actor> list = (List<Actor>)BTTreeManager.Instance.GetData(this, Define.BT_JUDGE_LIST);
            if (list == null)
            {
                return false;
            }

            switch (Damage)
            {
                case EDamageType.NONE:
                    {

                    }
                    break;
                case EDamageType.PHYS:
                case EDamageType.ARCANE:
                case EDamageType.FIRE:
                case EDamageType.ICE:
                case EDamageType.DARK:
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            Actor actor = list[i];
                            //int dmg = Owner.GetCurrAttr().GetAttr(EAttr.Atk);
                            //dmg = (int)(dmg * Percent);
                            //Owner.Attack(actor, dmg
                            Owner.Attack(actor, 5);
                        }
                    }
                    break;
                case EDamageType.HEAL:
                    //{
                    //    for (int i = 0; i < list.Count; i++)
                    //    {
                    //        Actor actor = list[i];
                    //        int dmg = actor.GetCurrAttr().GetAttr(EAttr.Atk);
                    //        dmg = (int)(dmg * Percent);
                    //        actor.AddHP(dmg, true);
                    //    }
                    //}
                    break;

            }
            return true;
        }

        public override BTNode DeepClone()
        {
            Hurt pNode = new Hurt();
            pNode.Damage = Damage;
            pNode.Percent = Percent;
            return pNode;
        }
    }
}

