using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardMonster : BoardActor
{
    private UISlider mHpSlider;
    private UILabel mName;

    public override void Init()
    {
        base.Init();
        mHpSlider = transform.FindChild("HPSlider").GetComponent<UISlider>();
        mName = transform.FindChild("Name").GetComponent<UILabel>();
    }

    public override void Refresh()
    {
        Actor actor = Owner as Actor;
        int maxHp = 1000;// actor.GetAttr(EAttr.MaxHP);
        int hp = 1000;// actor.GetAttr(EAttr.HP);
        if (maxHp > 0)
        {
            mHpSlider.value = hp / (maxHp * 1f);
        }
        else
        {
            mHpSlider.value = 0;
        }
        mName.text = "敌人";//actor.GetActorCard().Name;
    }
}
