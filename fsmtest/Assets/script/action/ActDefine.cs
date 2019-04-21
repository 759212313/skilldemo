using UnityEngine;
using System.Collections;

public enum EActType
{
    TYPE_NONE      = 0,
    TYPE_ADDATTR   = 1,  //增加属性
    TYPE_SUBATTR   = 2,  //减少属性
    TYPE_LDDATTR   = 3,  //间隔增加属性
    TYPE_LUBATTR   = 4,  //间隔减少属性
    TYPE_SUPER     = 5,  //霸体
    TYPE_VARIATION = 6,  //变形
    TYPE_STUN      = 7,  //昏迷
    TYPE_FIXBODY   = 8,  //定身
    TYPE_STEALTH   = 9,  //隐身
    TYPE_FROZEN    = 10, //冻住
    TYPE_BLIND     = 11, //致盲
    TYPE_SILENT    = 12, //沉默
    TYPE_SLEEP     = 13, //睡眠
    TYPE_ABSORB    = 14, //吸收伤害
    TYPE_WILD      = 15, //狂暴
    TYPE_DIVIVE    = 16, //无敌
    TYPE_PARALY    = 17, //麻痹
    TYPE_FEAR      = 18, //恐惧
    TYPE_REFLEX    = 19, //反弹伤害
    TYPE_DEAD      = 20, //倒计时死亡

    TYPE_ADDBUFF   = 21,  //添加BUFF
    TYPE_DISPEL    = 22,  //驱散
    TYPE_HITFLY    = 23,  //击飞
    TYPE_HITDOWN   = 24,  //击倒
    TYPE_HITBACK   = 25,  //击退
    TYPE_ANIM      = 26,  //动画
    TYPE_SOUND     = 27,  //声音
    TYPE_CHARM     = 28,  //蓄力
    TYPE_CAMERA    = 29,  //相机特效
    TYPE_SUMMON    = 30,  //召唤
    TYPE_FLASH     = 31,  //闪现
    TYPE_SPURT     = 32,  //冲锋
    TYPE_FLOAT     = 33,  //浮空

    TYPE_PUSH      = 34,  //推人
    TYPE_EFFECT    = 35,  //特效
    TYPE_TIP       = 36,  //技能提示
    TYPE_TAUNT     = 37,  //嘲讽
    TYPE_COLOR     = 38,  //变色
    TYPE_SHADER    = 39,  //Shader效果
    TYPE_SCALE     = 40,  //变换大小
    TYPE_OBJ       = 41,  //创建一个物体
}

public enum ESkillCostType
{
    NO,
    MP=1,//魔法
    HP=2,//生命
    SP=3,//特殊资源
    IP=4,//物品
    XP=5,//经验
}

public enum ESkillType
{
    Postive=0,//主动
    Passive=1,//被动
}


public enum EDamage
{
    TYPE_NONE,
    TYPE_PHYSICS = 1,//物理
    TYPE_HEAL    = 2,//治疗
    TYPE_FIRE    = 3,//火焰
    TYPE_ICE     = 4,//冰霜
    TYPE_DARK    = 5,//暗影
    TYPE_LIGHT   = 6 //闪电
}

public enum EActPosition
{
    Caster = 1,
    Target = 2
}

public enum EAoeArea
{
    TYPE_CYLINDER = 1,
    TYPE_SPHERE   = 2,
    TYPE_RECT     = 3,
}

public enum ESelectTargetPolicy
{
    Normal = 0,
    MaxHP  = 1,
    MinHP  = 2,
    MaxDis = 3,
    MinDis = 4,
}

public enum ECameraShake
{
    H,
    V,
    A
}

public enum EActStatus
{
    INITIAL,
    SUCCESS,
    FAILURE,
    RUNNING,
}