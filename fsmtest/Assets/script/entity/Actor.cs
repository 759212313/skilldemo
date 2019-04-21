using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Actor : ICharacter {

    #region 字段
    protected XTransform mBornParam;
    protected ActorPathFinding mActorPathFinding;
    protected Actor mHost;         //主人
    protected IStateMachine<Actor, FSMState> mMachine;
    protected ZTAction mActorAction;
    protected Animator mActorAnimator;
    protected ActorSkill mActorSkill;
    protected CharacterController mCharacter;
    protected ActorBehavior mBehavior;
    protected List<Actor> mEnemys = new List<Actor>();
    protected Actor mTarget;       //当前目标
    protected Transform[] mHands;
    protected ActorAI mActorAI;
    private EActorType type;
    private EBattleCamp camp;
    private int id;
    private int guid;
    public float speed = 1;
    public EActorType ActorType { get; private set; }
    public EMonsterType MonsterType { get; private set; }
    public EBattleCamp Camp { get; set; }

    public FSMState FSM
    {
        get
        {
            if (this.mMachine == null)
            {
                return FSMState.FSM_BORN;
            }
            return (FSMState)this.mMachine.GetCurrStateID();
        }
    }

    public Vector3 Pos
    {
        get { return this.CacheTransform.position; }
    }

    public float Radius
    {
        get { return mCharacter.radius * CacheTransform.localScale.x; }
    }

    public float Height
    {
        get { return mCharacter.height * CacheTransform.localScale.x; }
    }

    public Actor GetTarget()
    {
        return mTarget;
    }

    public Vector3 Dir
    {
        get { return CacheTransform.forward; }
    }

    public ActorAI GetActorAI()
    {
        return mActorAI;
    }
   
    public XTransform GetBornParam()
    {
        return mBornParam;
    }

    public ZTAction GetActorAction()
    {
        return mActorAction;
    }

    public ActorSkill GetActorSkill()
    {
        return mActorSkill;
    }
    #endregion

    #region 重写

    public override void Init()
    {
        Debug.LogError("Init");
        mActorPathFinding = new ActorPathFinding(this);
        this.InitFSM();
        this.InitAction();
        this.InitAI();
        this.AddCommands();
        this.InitBehavior();
        this.CreateBoard();
        ChangeEquip();
    }

    public override void Destroy()
    {
        //throw new NotImplementedException();
    }

    public override void Clear()
    {
        //throw new NotImplementedException();
    }

    public override void Load(XTransform param)
    {
        AddCommands();

        //mActorAct = new ActorAct(this);
        mActorSkill = new ActorSkill(this);
        this.Obj = ZTPool.Instance.GetGo("Model/Actor/" + this.Id);
        //this.Obj = ZTPool.Instance.GetGo("Model/Actor/50006");
        if (this.Obj == null)
        {
            Debug.LogError("对象为空");
            return;
        }
        this.Obj.transform.localPosition = Vector3.zero;
        this.mBornParam = param;
        this.CacheTransform = Obj.transform;
        this.mCharacter = Obj.GetComponent<CharacterController>();
        this.Init();
    }

    public override void Step()
    {
        if (CacheTransform == null || mMachine == null)
        {
            return;
        }

        mMachine.Step();
        //mActorBuff.Step();
        mActorPathFinding.Step();
        mActorAI.Step();
        mActorSkill.Step();
    }

    public override void ChangeModel(int modelID)
    {
        throw new NotImplementedException();
    }

    public override bool IsDead()
    {
        return FSM == FSMState.FSM_DEAD;
    }

    public override bool IsDestroy()
    {
        return CacheTransform == null;
    }

    public override void Pause(bool pause)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region 初始化
    public Actor(int id, int guid, EActorType type, EBattleCamp camp) : base(id, guid)
    {
        this.ActorType = type;
        this.Camp = camp;
        //this.MonsterType = ZTConfig.Instance.GetDBEntiny(Id).MonsterType;
    }

    protected void InitBehavior()
    {
        this.mBehavior = Obj.GET<ActorBehavior>();
        this.mBehavior.SetOwner(this);
    }

    protected void InitFSM()
    {
        this.mMachine = new IStateMachine<Actor, FSMState>(this);
        this.mMachine.AddState(FSMState.FSM_EMPTY, new ActorEmptyFSM());
        this.mMachine.AddState(FSMState.FSM_IDLE, new ActorIdleFSM());
        this.mMachine.AddState(FSMState.FSM_RUN, new ActorRunFSM());
        //this.mMachine.AddState(FSMState.FSM_WALK, new ActorWalkFSM());
        //this.mMachine.AddState(FSMState.FSM_TURN, new ActorTurnFSM());

        this.mMachine.AddState(FSMState.FSM_SKILL, new ActorSkillFSM());
        //this.mMachine.AddState(FSMState.FSM_DEAD, new ActorDeadFSM());

        //this.mMachine.AddState(FSMState.FSM_BEATFLY, new ActorBeatFlyFSM());
        //this.mMachine.AddState(FSMState.FSM_BEATBACK, new ActorBeatBackFSM());
        //this.mMachine.AddState(FSMState.FSM_BEATDOWN, new ActorBeatDownFSM());

        //this.mMachine.AddState(FSMState.FSM_WOUND, new ActorWoundFSM());
        //this.mMachine.AddState(FSMState.FSM_STUN, new ActorStunFSM());
        //this.mMachine.AddState(FSMState.FSM_FROST, new ActorFrostFSM());
        //this.mMachine.AddState(FSMState.FSM_FIXBODY, new ActorFixBodyFSM());
        //this.mMachine.AddState(FSMState.FSM_FEAR, new ActorFearFSM());
        //this.mMachine.AddState(FSMState.FSM_VARIATION, new ActorVariationFSM());
        //this.mMachine.AddState(FSMState.FSM_JUMP, new ActorJumpFSM());
        //this.mMachine.AddState(FSMState.FSM_REBORN, new ActorRebornFSM());
        //this.mMachine.AddState(FSMState.FSM_MINE, new ActorMineFSM());
        //this.mMachine.AddState(FSMState.FSM_INTERACTIVE, new ActorInterActiveFSM());
        this.mMachine.SetCurrState(this.mMachine.GetState(FSMState.FSM_IDLE));
        this.mMachine.GetState(this.mMachine.GetCurrStateID()).Enter();
    }

    protected void InitAction()
    {
        //DBEntiny db = ZTConfig.Instance.GetDBEntiny(Id);
        string ctrlstr = "Model/Actor/" + this.Id + "_Ctrl";
        //string ctrlstr = "Model/Actor/50006_Ctrl";
        //if (db == null)
        //{
        //    return;
        //}
        //mActorAnimator = GTTools.LoadAnimator(Obj, db.Ctrl);
        mActorAnimator = GTTools.LoadAnimator(Obj, ctrlstr);
        if (mActorAnimator != null)
        {
            mActorAnimator.enabled = true;
            mActorAnimator.applyRootMotion = true;
            mActorAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
            this.mActorAction = new ZTAction(mActorAnimator);
        }
    }

    protected void InitAI()
    {
        mActorAI = new ActorAI(this);
        mActorAI.Start();
    }
    #endregion

    #region 监听状态切换
    private void AddCommands()
    {
        this.Receiver.AddCommand<IDCommand>(ECommand.TYPE_IDLE, CheckIdle);
        this.Receiver.AddCommand<RTCommand>(ECommand.TYPE_RUNTO, CheckRunTo);
        this.Receiver.AddCommand<USCommand>(ECommand.TYPE_USESKILL, CheckUseSkill);
        //this.Receiver.AddCommand<DECommand>(ECommand.TYPE_DEAD, CheckDead);
        //this.Receiver.AddCommand<TNCommand>(ECommand.TYPE_TURN, CheckTurnTo);
        this.Receiver.AddCommand<MVCommand>(ECommand.TYPE_MOVETO, CheckMoveTo);
        //this.Receiver.AddCommand<TKCommand>(ECommand.TYPE_TALK, CheckTalk);
        //this.Receiver.AddCommand<FSCommand>(ECommand.TYPE_FROST, CheckFrost);
        //this.Receiver.AddCommand<HMCommand>(ECommand.TYPE_STUN, CheckStun);
        //this.Receiver.AddCommand<MBCommand>(ECommand.TYPE_PALSY, CheckPalsy);
        //this.Receiver.AddCommand<SPCommand>(ECommand.TYPE_SLEEP, CheckSleep);
        //this.Receiver.AddCommand<ZMCommand>(ECommand.TYPE_BLIND, CheckBlind);
        //this.Receiver.AddCommand<FRCommand>(ECommand.TYPE_FEAR, CheckFear);
        //this.Receiver.AddCommand<FBCommand>(ECommand.TYPE_FIXBODY, CheckFixBody);
        //this.Receiver.AddCommand<WDCommand>(ECommand.TYPE_WOUND, CheckWound);
        //this.Receiver.AddCommand<BBCommand>(ECommand.TYPE_BEATBACK, CheckBeatBack);
        //this.Receiver.AddCommand<BDCommand>(ECommand.TYPE_BEATDOWN, CheckBeatDown);
        //this.Receiver.AddCommand<BFCommand>(ECommand.TYPE_BEATFLY, CheckBeatFly);
        //this.Receiver.AddCommand<FLCommand>(ECommand.TYPE_FLOAT, CheckFly);
        //this.Receiver.AddCommand<HKCommand>(ECommand.TYPE_HOOK, CheckHook);
        //this.Receiver.AddCommand<GBCommand>(ECommand.TYPE_GRAB, CheckGrab);
        //this.Receiver.AddCommand<VACommand>(ECommand.TYPE_VARIATION, CheckVariation);
        //this.Receiver.AddCommand<ERCommand>(ECommand.TYPE_RIDE, CheckRide);
        //this.Receiver.AddCommand<JPCommand>(ECommand.TYPE_JUMP, CheckJump);
        //this.Receiver.AddCommand<YSCommand>(ECommand.TYPE_STEALTH, CheckSteal);
        //this.Receiver.AddCommand<RBCommand>(ECommand.TYPE_REBORN, CheckReborn);
        //this.Receiver.AddCommand<CJCommand>(ECommand.TYPE_MINE, CheckMine);
        //this.Receiver.AddCommand<ITCommand>(ECommand.TYPE_INTERACTIVE, CheckInterActive);
    }

    protected virtual ECommandReply CheckIdle(IDCommand cmd)
    {
        if (this.CacheTransform == null)
        {
            return ECommandReply.N;
        }
        //if (CannotControlSelf())
        //{
        //    return ECommandReply.N;
        //}
        if (FSM == FSMState.FSM_SKILL)
        {
            return ECommandReply.N;
        }
        SendStateMessage(FSMState.FSM_IDLE, cmd);
        return ECommandReply.Y;
    }

    //强制移动
    protected virtual ECommandReply CheckMoveTo(MVCommand cmd)
    {
        //if (CannotControlSelf())
        //{
        //    return ECommandReply.N;
        //}
        if (FSM == FSMState.FSM_SKILL)
        {
            return ECommandReply.N;
        }
        //if (GetAIFeature(EAIFeatureType.CAN_MOVE) == false)
        //{
        //    return ECommandReply.N;
        //}
        //if (this is ActorPlayer)
        //{
        //    this.GetActorAI().AIMode = EAIMode.Hand;
        //    SendStateMessage(FSMState.FSM_RUN, cmd);
        //    return ECommandReply.Y;
        //}
        //return ECommandReply.N;

        //Debug.LogError("开始移动");
        SendStateMessage(FSMState.FSM_RUN, cmd);
        return ECommandReply.Y;
    }

    //使用技能
    protected virtual ECommandReply CheckUseSkill(USCommand cmd)
    {
        if (this.CacheTransform == null)
        {
            return ECommandReply.N;
        }
        //if (CannotControlSelf())
        //{
        //    ShowWarning("100012");
        //    return ECommandReply.N;
        //}
        if (FSM == FSMState.FSM_SKILL)
        {
            return ECommandReply.N;
        }
        //if (GetActorEffect(EActorEffect.Is_Ride))
        //{
        //    ShowWarning("100011");
        //    return ECommandReply.N;
        //}

        //if (Define.USE_NEW_SKILL)
        //{
        //   ActSkill skill = this.mActorAct.GetSkill(cmd.Pos);
        //    if (skill == null) return ECommandReply.N;
        //    if (skill.IsInCD()) return ECommandReply.N;
        //    switch (skill.Data.CostType)
        //    {
        //        case ESkillCostType.HP:
        //            {
        //                bool success = UseHP(skill.Data.CostNum);
        //                if (!success)
        //                {
        //                    return ECommandReply.N;
        //                }
        //            }
        //            break;
        //        case ESkillCostType.MP:
        //            {
        //                bool success = UseMP(skill.Data.CostNum);
        //                if (!success)
        //                {
        //                    return ECommandReply.N;
        //                }
        //            }
        //            break;
        //    }
        //    cmd.LastTime = skill.Data.Dur;
        //}
        //else
        //{
        SkillTree skill = this.mActorSkill.GetSkill(cmd.Pos);
        //    if (skill == null) return ECommandReply.N;
        //    if (skill.IsInCD()) return ECommandReply.N;
        //    switch (skill.CostType)
        //    {
        //        case ESkillCostType.HP:
        //            {
        //                bool success = UseHP(skill.CostNum);
        //                if (!success)
        //                {
        //                    return ECommandReply.N;
        //                }
        //            }
        //            break;
        //        case ESkillCostType.MP:
        //            {
        //                bool success = UseMP(skill.CostNum);
        //                if (!success)
        //                {
        //                    return ECommandReply.N;
        //                }
        //            }
        //            break;
        //    }
        cmd.LastTime = skill.StateTime;
        //}
        SendStateMessage(FSMState.FSM_SKILL, cmd);
        return ECommandReply.Y;
    }

    //寻路至
    protected virtual ECommandReply CheckRunTo(RTCommand cmd)
    {
        //if (CannotControlSelf())
        //{
        //    return ECommandReply.N;
        //}
        //if (FSM == FSMState.FSM_SKILL)
        //{
        //    return ECommandReply.N;
        //}
        //if (GetAIFeature(EAIFeatureType.CAN_MOVE) == false)
        //{
        //    return ECommandReply.N;
        //}
        //if (mVehicle.GetActorPathFinding().CanReachPosition(cmd.DestPosition) == false)
        //{
        //    ShowWarning("300001");
        //    return ECommandReply.N;
        //}
        this.GetActorAI().AIMode = EAIMode.Auto;
        SendStateMessage(FSMState.FSM_RUN, cmd);
        return ECommandReply.Y;
    }
    #endregion

    #region 切换状态

    public void SendStateMessage(FSMState fsm)
    {
        ChangeState(fsm, null);
    }

    public void SendStateMessage(FSMState fsm, ICommand ev)
    {
        ChangeState(fsm, ev);
    }

    public void ChangeState(FSMState fsm, ICommand ev)
    {
        if (mMachine == null || this.CacheTransform == null)
        {
            return;
        }
        if (FSM == FSMState.FSM_DEAD && fsm != FSMState.FSM_REBORN)
        {
            return;
        }
        if (!mMachine.Contains(fsm))
        {
            return;
        }
        mMachine.GetState(fsm).SetCommand(ev);
        mMachine.ChangeState(fsm);
    }

    public void GotoEmptyFSM()
    {
        ChangeState(FSMState.FSM_EMPTY, null);
    }
    #endregion

    #region 执行动作
    //追赶
    public virtual void OnPursue(RTCommand ev)
    {
        this.mActorPathFinding.SetOnFinished(ev.Callback);
        MoveTo(ev.DestPosition);
        this.mActorAction.Play("run", null, true);
    }

    public virtual void OnUseSkill(USCommand ev)
    {

        //if (this.Camp == EBattleCamp.A)
        //{
        //    return;
        //}
        //Util.Instance.Log("===========使用技能======");
        //Debug.LogError("使用技能");
        StopPathFinding();
        LookAtEnemy();
        this.mActorSkill.UseSkill(ev.Pos);
    }

    public virtual int Attack(Actor defender, int value)
    {
        if (defender == null || value <= 0)
        {
            return 0;
        }
        //float v = (value - defender.GetAttr(EAttr.Def) * 0.2f);
        //v = v * Define.DAMAGE_RATIO;
        //if (v <= 0)
        //{
        //    v = UnityEngine.Random.Range(3, 7);
        //}

        //float cRate = this.GetAttr(EAttr.Crit) * 0.01f;
        //float bRate = this.GetAttr(EAttr.CritDamage) * 0.01f;
        //bool critical = GTTools.IsTrigger(cRate);
        //if (critical)
        //{
        //    v = (v * (1 + bRate));
        //}
        //int dmg = Mathf.FloorToInt(UnityEngine.Random.Range(0.85f, 1.08f) * v);
        //Debug.LogError("敌人掉血====>" + dmg);
        //defender.BeDamage(this, dmg, critical);
        //return dmg;
        defender.BeDamage(this, value, false);
        return value;
    }

    public virtual void MoveTo(Vector3 destPosition)
    {
        //this.SetActorEffect(EActorEffect.IS_AutoToMove, true);
        mActorPathFinding.SetDestPosition(destPosition);
    }

    public virtual void StopPathFinding()
    {
        //this.SetActorEffect(EActorEffect.IS_AutoToMove, false);
        //Debug.LogError("停止寻路");
        mActorPathFinding.StopPathFinding();
    }

    public void OnForceToMove(MVCommand ev)
    {
        Vector2 delta = ev.Delta;
        this.CacheTransform.LookAt(new Vector3(this.CacheTransform.position.x + delta.x, this.CacheTransform.position.y, this.CacheTransform.position.z + delta.y));
        mCharacter.SimpleMove(mCharacter.transform.forward * speed);
        this.mActorAction.Play("run", null, true);
    }

    public void OnIdle()
    {
        if (mActorAnimator != null)
        {
            this.mActorAction.Play("idle", null, true);
        }
    }

    public void LookAtEnemy()
    {
        if (mTarget == null || mTarget.IsDead() )//|| !IsEnemy(mTarget) || mTarget.GetActorEffect(EActorEffect.IS_Stealth) == true)
        {
            mTarget = null;
        }
        Actor enemy = GetNearestEnemy(mActorAI.WARDIST);
        this.SetTarget(enemy);
        if (mTarget != null)
        {
            CacheTransform.LookAt(new Vector3(mTarget.Pos.x, Pos.y, mTarget.Pos.z));
        }
    }

    public void OnArrive()
    {
        GotoEmptyFSM();
        //if (mHost != null && mHost.GetActorEffect(EActorEffect.Is_Ride))
        //{
        //    mHost.OnArrive();
        //}
        //mHost.OnArrive();
    }

    public void BeDamage(Actor attacker, int damage, bool critial = false)
    {
        //Debug.LogError("掉血飘字" + damage);
        //掉血飘字
        ShowFlywordByDamage(attacker, damage, critial);
        //if (this.mCurrAttr.HP > damage)
        //{
        //    UpdateAttr(EAttr.HP, this.mCurrAttr.HP - damage);
        //}
        //else
        //{
        //    UpdateAttr(EAttr.HP, 0);
        //}
        //UpdateHealth();
        //if (this.mCurrAttr.HP <= 0)
        //{
        //    Command(new DECommand(EDeadReason.Normal));
        //}
    }
    #endregion

    #region 拓展方法
    public void ApplyRootMotion(bool enabled)
    {
        if (mActorAnimator != null)
        {
            mActorAnimator.applyRootMotion = enabled;
        }
    }

    public void SetTarget(Actor actor)
    {
        if (actor == null)
        {
            this.mTarget = null;
            return;
        }
        if (mTarget == actor)
        {
            return;
        }
        this.mTarget = actor;
        this.CacheTransform.LookAt(this.mTarget.CacheTransform);
    }

    public Actor GetNearestEnemy(float radius = 10000)
    {
        List<Actor> actors = GetAllEnemy();
        //Debug.LogError("敌人列表"+actors.Count);
        Actor nearest = null;
        float min = radius;
        for (int i = 0; i < actors.Count; i++)
        {
            float dist = GTTools.GetHorizontalDistance(actors[i].CacheTransform.position, this.CacheTransform.position);
            if (dist < min)
            {
                min = dist;
                nearest = actors[i];
            }
        }
        return nearest;
    }

    public List<Actor> GetAllEnemy()
    {
        mEnemys.Clear();
        FindActorsByTargetCamp(ETargetCamp.Enemy, ref mEnemys);
        return mEnemys;
    }

    public void FindActorsByTargetCamp(ETargetCamp actorCamp, ref List<Actor> list, bool ignoreStealth = false)
    {
        for (int i = 0; i < Laucher.instance.AllActors.Count; i++)
        {
            Actor actor = Laucher.instance.AllActors[i];

            //Debug.Log("玩家阵营" + GetTargetCamp(actor).ToString());
            if (GetTargetCamp(actor) == actorCamp && actor.IsDead() == false)
            {
                if (ignoreStealth == false)
                {
                    list.Add(actor);
                }
                else
                {
                    //if (actor.GetActorEffect(EActorEffect.IS_Stealth) == false)
                    //{
                    //    list.Add(actor);
                    //}
                }
            }
        }
    }

    public ETargetCamp GetTargetCamp(Actor actor)
    {
        if (actor.Camp == EBattleCamp.D)
        {
            return ETargetCamp.None;
        }
        if (actor.Camp == Camp)
        {
            return ETargetCamp.Ally;
        }
        if (actor.Camp != EBattleCamp.C && Camp != EBattleCamp.C)
        {
            return ETargetCamp.Enemy;
        }
        return ETargetCamp.Neutral;
    }

    public List<Actor> GetActorsByAffectType(EAffect type)
    {
        switch (type)
        {
            //case EAffect.Ally:
            //    return GetAllAlly();
            //case EAffect.Host:
            //    if (mHost == null)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        return new List<Actor>() { mHost };
            //    }
            case EAffect.Enem:
                return GetAllEnemy();
            //case EAffect.Boss:
            //    List<Actor> list = new List<Actor>();
            //    List<Actor> enemys = GetAllEnemy();
            //    for (int i = 0; i < enemys.Count; i++)
            //    {
            //        Actor monster = enemys[i];
            //        if (monster.ActorType == EActorType.MONSTER)
            //        {
            //            if (monster.IsBoss())
            //            {
            //                list.Add(monster);
            //            }
            //        }
            //    }
            //    return list;
            //case EAffect.Self:
            //    return new List<Actor>() { this };
            //case EAffect.Each:
            //    return LevelData.AllActors;
            default:
                return new List<Actor>();
        }
    }

    protected void ShowFlywordByDamage(Actor attacker, int damage, bool critial)
    {
        //if ((Camp == EBattleCamp.A && IsAvatar()) || attacker == null)
        if ((Camp == EBattleCamp.A) || attacker == null)
            {
            if (critial)
            {
                ShowFlyword(EFlyWordType.TYPE_ENEMY_CRIT, damage);
            }
            else
            {
                ShowFlyword(EFlyWordType.TYPE_ENEMY_HURT, damage);
            }
        }
        else
        {
            switch (attacker.ActorType)
            {
                case EActorType.PLAYER:
                    if (critial)
                    {
                        ShowFlyword(EFlyWordType.TYPE_AVATAR_CRIT, damage);
                    }
                    else
                    {
                        ShowFlyword(EFlyWordType.TYPE_AVATAR_HURT, damage);
                    }
                    break;
                case EActorType.PARTNER:
                    if (critial)
                    {
                        ShowFlyword(EFlyWordType.TYPE_PARTNER_CRIT, damage);
                    }
                    else
                    {
                        ShowFlyword(EFlyWordType.TYPE_PARTNER_HURT, damage);
                    }
                    break;
            }
        }
    }

    protected void ShowFlyword(EFlyWordType wordType, int value)
    {
        //if (this.Camp == EBattleCamp.A)
        //{
        //    return;
        //}

        if (IsDead())
        {
            return;
        }
        Vector3 temp = CacheTransform.position;
        //Debug.LogError(temp.x + "  " + temp.y + "  "+temp.z+"  "+ this.Camp.ToString());
        ZTFlyword.Instance.Show(value.ToString(), CacheTransform.position, wordType);
    }

    protected void CreateBoard()
    {
        //if (this is ActorPlayer && Launcher.Instance.GetCurrSceneType() != ESceneType.City)
        //{
        //    return;
        //}
        switch (ActorType)
        {
            case EActorType.PLAYER:
                ZTBoard.Instance.Create(this, EBoardType.TYPE_PLAYER);
                break;
            case EActorType.NPC:
                ZTBoard.Instance.Create(this, EBoardType.TYPE_NPC);
                break;
            case EActorType.MONSTER:
                ZTBoard.Instance.Create(this, EBoardType.TYPE_MONSTER);
                break;
        }
    }

    public void ChangeEquip()
    {
        //DBItem itemDB = ZTConfig.Instance.GetDBItem(pEquipID);
        //string[] modelPaths = { itemDB.Model_R, itemDB.Model_L };
        //EquipAvatar pModel = GetEquipModelsByPos(pDressPos);
        //for (int i = 0; i < 2; i++)
        {
            string path = "Model/Weapon/120004";//modelPaths[i];
            string bone = Define.EQUIP_BONES[7, 0];
            //if (string.IsNullOrEmpty(path)) { continue; }
            Transform boneTrans = GTTools.GetBone(CacheTransform, bone);
            if (boneTrans == null) { return; }
            //pModel.Models[i] = ZTResource.Instance.Load<GameObject>(path, true);
            GameObject eObj = ZTResource.Instance.Load<GameObject>(path, true);
            if (eObj != null)
            {
                GameObject model = eObj;
                model.transform.parent = boneTrans;
                NGUITools.SetLayer(eObj, Obj.layer);
                model.transform.localPosition = Vector3.zero;
                model.transform.localEulerAngles = Vector3.zero;
                model.transform.localScale = Vector3.one;
            }
        }
    }

    //获取绑定位置
    public Vector3 GetBind(EActorBindPos bind, Vector3 offset)
    {
        switch (bind)
        {
            case EActorBindPos.Body:
                return Pos + new Vector3(0, Height * 0.5f, 0) + offset;
            case EActorBindPos.Head:
                return Pos + new Vector3(0, Height, 0) + offset;
            case EActorBindPos.Foot:
                return Pos + offset;
            default:
                return Pos;
        }
    }

    //获取手掌列表
    public Transform[] GetHands()
    {
        if (mHands == null && CacheTransform != null)
        {
            mHands = new Transform[2];
            mHands[0] = GTTools.GetBone(CacheTransform, "Bip01 L Hand");
            mHands[1] = GTTools.GetBone(CacheTransform, "Bip01 R Hand");
        }
        return mHands;
    }
    #endregion
}
