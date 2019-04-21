using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum EffectLodLevel
{
    Low,
    High
}

public enum EAction
{
    None   = 0,
    Manual = 1,
    Energy = 2,
}

public enum EAnim
{
    Animate,
    Animator,
}


public enum EProduct
{
    ITEM,
    GIFT,
    RARE,
}

public enum EMoney
{
    Gold    = 1,
    Diam    = 2,
    Crystal = 3,
    Glyph   = 4,
    Honor   = 5,
    Hoon    = 6,
    Fame    = 7,
    Mete    = 8,
    Apple   = 9,
    Soul    = 10,
    Goul    = 11,
    Poul    = 12
}

public enum EItemType
{
    EQUIP    = 1,
    DRUG     = 2,
    MAT      = 3,
    BOX      = 4,
    KEY      = 5,
    CHIP     = 6,
    GEM      = 7,
    FASHION  = 8,
    MONEY    = 9,
    ACTION   =10,
    EXP      =11,
    RUNE     =12,
    PETSOUL  =13,  
    TASK     =14,
}

public enum EBagType
{
    NONE    = 0,
    ITEM    = 1,
    GEM     = 2,
    FASHION = 3,
    CHIP    = 4,
    RUNE    = 5,
    TASK    = 6,
    SOUL    = 7,
    CARD    = 8  //英雄卡
}

public enum ECarrer
{
    O=0,  //通用
    A=1,  //圣骑士
    B=2,  //法师
    C=3,  //刺客
    D=4   //狂战士
}


public enum ERuneType
{
    LIT = 1,//小雕文
    MID = 2,//中雕文
    BIG = 3 //大雕文
}

public enum ESceneType
{
    Init,
    Login,
    Role,
    City,
    Battle,
    Farm,
}

public enum EAIState
{
    AI_NONE,  //无
    AI_IDLE,  //闲逛
    AI_FIGHT, //战斗
    AI_FOLLOW,//跟随
    AI_PATROL,//巡逻
    AI_DEAD,  //死亡
    AI_BACK,  //回家
    AI_CHASE, //追击
    AI_FLEE,  //避开
    AI_ESCAPE,//逃跑
    AI_BORN,  //出生
    AI_PLOT,  //剧情
    AI_GLOBAL,//全局
}

public enum EAITarget
{
    TYPE_SELF  =0,
    TYPE_TARGET=1,
    TYPE_HOST  =2,
}

public enum FSMState : int
{
    FSM_EMPTY,
    FSM_BORN,                //出生
    FSM_IDLE,                //待机
    FSM_TURN,                //转向

    FSM_WALK,                //漫步
    FSM_RUN,                 //跑

    FSM_SKILL,               //攻击
    FSM_DEAD,                //死亡
    FSM_REBORN,              //重生

    FSM_WOUND,               //受击
    FSM_BEATBACK,            //击退
    FSM_BEATDOWN,            //击倒
    FSM_BEATFLY,             //击飞
    FSM_FLOATING,            //浮空

    FSM_FROST,               //冰冻
    FSM_STUN,                //昏迷
    FSM_FIXBODY,             //定身
    FSM_VARIATION,           //变形
    FSM_FEAR,                //恐惧
    FSM_SLEEP,               //睡眠
    FSM_PARALY,              //麻痹
    FSM_BLIND,               //致盲

    FSM_PICK,                //捡起

    FSM_RIDEIDLE,            //骑乘闲置
    FSM_RIDERUN,             //骑乘跑

    FSM_DROP,                //下落
    FSM_TALK,                //说话
    FSM_HOOK,                //钩子
    FSM_GRAB,                //抓取
    FSM_FLY,                 //飞行
    FSM_RAGDOLL,             //布娃娃
    FSM_ROLL,                //翻滚
    FSM_JUMP,                //跳跃

    FSM_DANCE,               //跳舞
    FSM_MINE,                //采集状态
    FSM_INTERACTIVE,         //交互
}


public enum EAIMode
{
    Auto,      //自动
    Hand,      //手动
}

public enum ECompare
{
    EQ=0,//等于
    GT=1,//大于
    LT=2,//小于
    GE=3,//大于等于
    LE=4,//小于等于
    NO=5,//不等于
}

public enum EAIFeatureType
{
    CAN_MOVE,          //可移动
    CAN_KILL,          //可击杀
    CAN_MANUALATTACK,  //可主动攻击
    CAN_PURSUE,        //可寻路
    CAN_TURN,          //可转向
    CAN_STUN,          //可击晕
    CAN_ATTACK,        //可攻击
    CAN_BEATBACK,      //可击退
    CAN_BEATFLY,       //可击飞
    CAN_BEATDOWN,      //可击倒
    CAN_WOUND,         //可受击
    CAN_REDUCESPEED,   //可减速
    CAN_FIXBODY,       //可定身
    CAN_SLEEP,         //可睡眠
    CAN_VARISTION,     //可变形
    CAN_PARALY,        //可麻痹
    CAN_FEAR,          //可恐惧
}


public enum EActorEffect
{
    IS_AutoToMove,
    IS_Task,
    IS_Story,
    IS_Stealth,
    Is_Silent,
    Is_Divine,
    Is_Super,
    Is_Ride,
}


public enum EAttr
{
    MaxHP      =1,   //最大生命值  
    HP         =2,   //生命值
    MaxMP      =3,   //最大魔法值
    MP         =4,   //魔法值
    Atk        =5,   //攻击力
    Def        =6,   //防御力
    Speed      =7,   //速度
    Crit       =8,   //爆击
    CritDamage =9,   //爆击伤害
    SuckBlood  =10,  //吸血
    HPRecover  =11,  //回血
    MPRecover  =12,  //回魔
    Dodge      =13,  //闪避
    Hit        =14,  //命中
    Absorb     =15,  //伤害吸收
    Reflex     =16,  //伤害反弹
}


public enum EProperty
{
    LHP = 1,   //生命值
    ATK = 2,   //攻击力
    DEF = 3,   //防御力
    CRI = 4,   //爆击
    BUR = 5,   //爆伤
    LMP = 6,   //魔法值
    VAM = 7,   //吸血
    HIT = 8,   //命中
    DOG = 9,   //闪避
    BAF = 10,  //爆防
    PAP = 11,  //攻击百分比
    PHP = 12,  //生命百分比
    PDF = 13,  //防御百分比
    PMP = 14,  //魔法百分比
    PMS = 15,  //免伤百分比
    PWD = 16,  //回血
    PBL = 17,  //回魔
    PEW = 18,  //额外伤害
    PRH = 19,  //攻击回血
    PRE = 20   //攻击回能
}

public enum ETargetCamp
{
    None,
    Ally,
    Enemy,
    Neutral,
}

public enum EActorSex
{
    B,
    G,
    X,
}

public enum EPartnerSort
{
    MD,
    LF,
    RT,
}

public enum EActorType
{
    PLAYER,   //玩家
    NPC,      //NPC
    MONSTER,  //怪物
    PET,      //宠物
    MOUNT,    //坐骑
    MACHINE,  //机关
    PARTNER,  //伙伴
}

public enum EDeadReason
{
    Normal,   //正常死亡
    Dot,      //Dot
    Kill,     //机制秒杀
    Plot      //剧情杀
}

public enum ESkillPos
{
    Skill_0,
    Skill_1,
    Skill_2,
    Skill_3,
    Skill_4,
    Skill_5,
    Skill_6,
    Skill_7,
    Skill_8,
    Skill_9,
}

public enum EAudioState
{
    Loading,//加载
    Wait,//准备
    FadeIn,//声音渐强
    FadeOut,//声音渐弱
    Play,//播放
    Stop,//停止播放，声音渐低
    Loop,//循环
    Finish,//完毕等待删除
}

public enum EAudioType
{
    Sound,
    Music,
}


//实体位置
public enum EActorBindPos
{
    Body=0,
    Head=1,
    Foot=2,
    Hand=3,
}

public enum EAffect
{
    None =0,//无
    Self =1,//影响自己
    Enem =2,//影响敌方
    Ally =3,//影响友方
    Each =4,//影响所有
    Boss =5,//影响Boss
    Host =6,//影响主人
}

public enum EValueType
{
    FIX,  //固定值
    PER,  //百分数
    COM,  //以最大值为基准百分比,作用于当前值
}

public enum EDropObjectAbsorbMode
{
    LineChase=1,           //直线追踪
    LineChaseAndCircle,    //先上升，然后旋转追踪目标吸附
}

public enum EDropObjectState
{
    None,
    Created,       //已创建
    Splash,        //四处溅射，宝物向四周随机角度抛物线弹出
    Raise,         //升高
    Wait,          //等待
    CircleFly,     //曲线飞向主角
    LineFly,       //直线飞向主角
    Dead,          //消亡
}


public enum EWeatherType
{
    NONE     =0,
    SUNNY    =1,   //晴天
    THUNDER  =2,   //打雷
    RAIN     =4,   //下雨
    SNOW     =8,   //下雪
    HAIL     =16,  //冰雹
    STORM    =32,  //风暴
}


//怪物类型
public enum EMonsterType
{
    None   = 0,
    Normal = 1,   //正常
    Elite  = 2,   //精英
    Rare   = 3,   //稀有
    Boss   = 4,   //Boss
    World  = 5,   //世界Boss
    Chest  = 6,   //宝箱
    Tower  = 7,   //水晶塔
    Cage   = 8,   //囚笼
}

//怪物种族
public enum EActorRace
{
    TYPE_NONE    =1,    //宝箱、囚笼等
    TYPE_HUMAN   =2,    //人类
    TYPE_SPIRIT  =3,    //精灵
    TYPE_ORC     =4,    //兽人
    TYPE_GHOST   =5,    //亡灵
    TYPE_DEVIL   =6,    //恶魔
    TYPE_ELEM    =7,    //元素
    TYPE_GIANT   =8,    //巨人
    TYPE_MACHINE =9  ,  //机械
    TYPE_BEAST   =10,   //野兽
    TYPE_DRAGON  =11,   //龙类
}

//触发器条件之间的关系
public enum EConditionRelation
{
    AND = 0,
    OR  = 1,
}


public enum EBattleCamp
{
    A,//我方
    B,//敌方
    C,//中立
    D,//其他
}

//机关类型
public enum EMechanism
{
    None,
    Move,
    Prick,
    Anim,
    Drop,
}

//Buff叠加类型
public enum EBuffOverlayType
{
    UnChange,
    Overlay,
    Reset,
    Cancle,
}

//Buff销毁类型
public enum EBuffDestroyType
{
    REMOVE_NONE,
    REMOVE_BATTLEEND,
    REMOVE_TIMEEND,
    REMOVE_OFFLINE,
    REMOVE_DEAD
}

public enum EBuffType
{
    None,
    Buff,
    Debuff,
}

//特效出现位置
public enum EEffectBind
{
    World = -1, //纯坐标
    Trans = 0, //在某一个物体下面
    OwnBody = 1, //出现在自身身体位置
    OwnHead = 2, //出现在自身头部位置
    OwnFoot = 3, //出现在自身脚部位置
    OwnHand = 4, //出现在自身手上
    TarBody = 5, //出现在目标身体位置
    TarHead = 6, //出现在目标头部位置
    TarFoot = 7, //出现在目标脚部位置
    OwnVTar = 8, //出现在目标与自身的连线中点
}


public enum EEffectState
{
    Wait,
    Update,
    Dead,
    Error,
}

public enum EEffectType
{
    UV,
    PA,
    EP,
    TR,
}

//范围类型
public enum ERegionType
{
    None = 0,       //锁定对象
    Sphere = 1,     //球体
    Box = 2,        //长方体
    Cylinder = 3,   //圆柱
}

//技能伤害类型
public enum EDamageType
{
    NONE = 0,
    PHYS = 1,//物理
    ARCANE = 2,//奥术
    HEAL = 3,//治疗
    FIRE = 4,//火焰
    ICE = 5,//冰霜
    DARK = 6,//暗影
    LIGHT = 7,//闪电
}

public enum ESkillBreakReason
{
    TYPE_NONE,      //无
    TYPE_BUFF,      //Buff打断
    TYPE_STIFF,     //硬直打断
    TYPE_NEWSKILL,  //新技能打断
    TYPE_BEDAMAGE,  //受到伤害打断
    TYPE_SPECIAL,   //特殊打断
}

public enum EFlyType
{
    STAY,    //停留原地
    LINE,    //直线
    PURSUE,  //追踪
    THROW,   //抛物线
    CROSS,   //回旋
    BACK,    //返回施法者
}

public enum EFlyObjDeadType
{
    UntilLifeTimeEnd = 0,    //生命周期结束
    UntilColliderTar = 1,    //触碰目标结束
    DirectDestroy = 2,    //暴力销毁
}