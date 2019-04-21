using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardPlayer : BoardActor
{
    private UILabel mLevelAndName;
    private UISprite mTitle;

    public override void Init()
    {
        base.Init();
        mLevelAndName = transform.FindChild("LevelAndName").GetComponent<UILabel>();
        mTitle = transform.FindChild("Title").GetComponent<UISprite>();
        mTitle.gameObject.SetActive(false);
    }

    public override void Refresh()
    {
        mLevelAndName.color = Color.green;
        mTitle.color = Color.white;
        Actor actor = Owner as Actor;
        //ActorCard pCard = actor.GetActorCard();
        mLevelAndName.text = "Lv.100 无敌";//GTTools.Format("Lv.{0} {1}", pCard.Level, pCard.Name);
    }
}
