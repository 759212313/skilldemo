using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace BT
{
    public class BeatFly :BTTask
    {
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
            //    actor.Command(new BFCommand());
            //}
            return true;
        }

        protected override EBTStatus Execute()
        {
            return EBTStatus.BT_SUCCESS;
        }

        public override BTNode DeepClone()
        {
            BeatFly data = new BeatFly();
            return data;
        }
    }
}

