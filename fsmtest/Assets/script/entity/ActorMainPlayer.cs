using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorMainPlayer : ActorPlayer
{
    public ActorMainPlayer(int id, int guid, EActorType type, EBattleCamp camp) : base(id, guid, type, camp)
    {

    }

    public override void Load(XTransform data)
    {
        base.Load(data);
        //ZTEvent.AddHandler<int, int>(EventID.RECV_UNLOAD_EQUIP,ChangeEquipAvatar);
        //ZTEvent.AddHandler<int, int>(EventID.RECV_DRESS_EQUIP, ChangeEquipAvatar);
        //ZTEvent.AddHandler(EventID.RECV_PLAYER_END_MOUNT, OnEndRide);
        //ZTEvent.AddHandler(EventID.CHANGE_HEROLEVEL, OnUpgradeLevel);
        //ZTEvent.AddHandler(EventID.CHANGE_FIGHTVALUE, OnChangeFightValue);
        //ZTEvent.AddHandler<int,int>(EventID.RECV_KILL_MONSTER, OnKillMonster);
        //ZTEvent.AddHandler<int,int>(EventID.RECV_CHANGE_PARTNER, OnChangePartner);
    }

    /*private void OnChangePartner(int pos, int id)
    {
        mActorCard.SetPartnerByPos(pos, id);
        switch (pos)
        {
            case 1:
                ZTLevel.Instance.DelActor(this.Partner1);
                break;
            case 2:
                ZTLevel.Instance.DelActor(this.Partner2);
                break;
        }
        ZTLevel.Instance.AddPartner(this, pos, id);
    }

    private void OnKillMonster(int guid,int id)
    {
        DBEntiny db = ZTConfig.Instance.GetDBEntiny(id);
        if(db.Exp>0)
        {
            ZSRole.Instance.TryAddHeroExp(db.Exp);
        }
    }

    private void OnUpgradeLevel()
    {
        this.GetActorCard().SetLevel();
        ZTBoard.Instance.Refresh(this);
        EffectData data = new EffectData();
        data.Owner = this;
        data.Id = Define.EFFECT_UPGRADE_ID;
        data.LastTime = 3;
        data.Bind = EEffectBind.OwnFoot;
        data.Dead = EFlyObjDeadType.UntilLifeTimeEnd;
        data.Parent = CacheTransform;
        data.SetParent = true;
        EffectBase effect = ZTEffect.Instance.CreateEffect(data);
    }

    public override void UpdateCurrAttr(bool init = false)
    {
        base.UpdateCurrAttr(init);
        ZTEvent.FireEvent(EventID.UPDATE_AVATAR_ATTR);
    }

    private void OnChangeFightValue()
    {
        InitAttr();
    }

    public override void Destroy()
    {
        base.Destroy();
        ZTEvent.RemoveHandler<int, int>(EventID.RECV_UNLOAD_EQUIP, ChangeEquipAvatar);
        ZTEvent.RemoveHandler<int, int>(EventID.RECV_DRESS_EQUIP, ChangeEquipAvatar);
        ZTEvent.RemoveHandler(EventID.RECV_PLAYER_END_MOUNT, OnEndRide);
        ZTEvent.RemoveHandler(EventID.CHANGE_HEROLEVEL, OnUpgradeLevel);
        ZTEvent.RemoveHandler(EventID.CHANGE_FIGHTVALUE, OnChangeFightValue);
        ZTEvent.RemoveHandler<int, int>(EventID.RECV_KILL_MONSTER, OnKillMonster);
        ZTEvent.RemoveHandler<int, int>(EventID.RECV_CHANGE_PARTNER, OnChangePartner);
    }*/
}
