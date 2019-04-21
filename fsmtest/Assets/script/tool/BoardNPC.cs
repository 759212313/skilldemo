using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardNpc : BoardActor
{
    private UILabel mName;
    private UILabel mTitle;

    public override void Init()
    {
        base.Init();
        mName = transform.FindChild("Name").GetComponent<UILabel>();
        mTitle = transform.FindChild("Title").GetComponent<UILabel>();
    }

    public override void Refresh()
    {
        Actor actor = Owner as Actor;
        //DBEntiny db = ZTConfig.Instance.GetDBEntiny(actor.Id);
        mName.color = new Color(0, 0.7f, 1, 1);
        mTitle.color = new Color(0, 0.7f, 1, 1);
        mName.text = "npc";//actor.GetActorCard().Name;
        //if (!db.Title.Equals(string.Empty))
        //{
        //    mTitle.text = "<" + db.Title + ">";
        //}
        //else
        //{
        //    mTitle.text = string.Empty;
        //}
    }
}
