using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum ECommand:byte
{
    TYPE_RUNTO,//寻路
    TYPE_TURN,//转向
    TYPE_USESKILL,//使用技能
    TYPE_LOADING,//加载场景
    TYPE_TALK,//谈话
    TYPE_DEAD,//死亡
    TYPE_MOVETO,//玩家摇杆强制移动

    TYPE_FROST,//冰冻
    TYPE_STUN,//昏迷
    TYPE_FLOAT,//浮空
    TYPE_VARIATION,//变形
    TYPE_PALSY,//麻痹
    TYPE_SLEEP,//睡眠
    TYPE_FIXBODY,//定身
    TYPE_GRAB,//被抓取
    TYPE_HOOK,//被勾取
    TYPE_ROLL,//滚地

    TYPE_BEATBACK,//被击退
    TYPE_BEATDOWN,//被击倒
    TYPE_BEATFLY,//被击飞

    TYPE_FEAR,//恐惧
    TYPE_FLY,//飞行
    TYPE_WOUND,//受击

    TYPE_BLIND,//致盲

    TYPE_RIDE,//骑上骑乘
    TYPE_JUMP,//跳跃
    TYPE_STEALTH,//隐身
    TYPE_SILENT,//沉默
    TYPE_IDLE,
    TYPE_REBORN,//重生
    TYPE_MINE,//采集
    TYPE_INTERACTIVE,//交互
}


public enum ECommandReply:byte
{
    Y,
    N,
}

public enum EPursueType:byte
{
    Position,
    Transform,
    Actor,
}

public class ICommand
{
    public ECommand Command { get; set; }
}


public class IDCommand:ICommand
{
    public IDCommand()
    {
        this.Command = ECommand.TYPE_IDLE;
    }
}


/// <summary>
/// 强制移动
/// </summary>
public class MVCommand : ICommand
{
    public Vector2 Delta;

    public MVCommand(Vector2 delta)
    {
        this.Delta = delta;
        this.Command = ECommand.TYPE_MOVETO;
    }
}

/// <summary>
/// 寻路
/// </summary>
public class RTCommand : ICommand
{
    public Vector3 DestPosition
    {
        get
        {
            switch (Pursue)
            {
                case EPursueType.Actor:
                    return TargetActor.CacheTransform.position;
                case EPursueType.Position:
                    return TargetPos;
                case EPursueType.Transform:
                    return Target.position;
            }
            return Vector3.zero;
        }
    }

    public EPursueType Pursue;
    public Vector3 TargetPos;
    public Transform Target = null;
    public Actor TargetActor = null;
    public Callback Callback;

    public RTCommand(Vector3 destPosition, Callback callback = null)
    {
        this.TargetPos = destPosition;
        this.Command = ECommand.TYPE_RUNTO;
        this.Pursue = EPursueType.Position;
        this.Callback = callback;
    }

    public RTCommand(Transform target, Callback callback = null)
    {
        this.Target = target;
        this.Command = ECommand.TYPE_RUNTO;
        this.Pursue = EPursueType.Transform;
        this.Callback = callback;
    }

    public RTCommand(Actor actor, Callback callback = null)
    {
        this.TargetActor = actor;
        this.Command = ECommand.TYPE_RUNTO;
        this.Pursue = EPursueType.Actor;
        this.Callback = callback;
    }
}

/// <summary>
/// 使用技能
/// </summary>
public class USCommand : ICommand
{
    public ESkillPos Pos = ESkillPos.Skill_0;
    public SkillTree Tree = null;
    public float LastTime;

    public USCommand(ESkillPos pos)
    {
        this.Pos = pos;
        this.Command = ECommand.TYPE_USESKILL;
    }
}

/*


/// <summary>
/// 转向
/// </summary>
public class TNCommand:ICommand
{
    public Vector3  LookDirection;
    public Callback OnFinish;

    public TNCommand(Vector3 lookDirection,Callback onFinish)
    {
        this.LookDirection = lookDirection;
        this.OnFinish = onFinish;
        this.Command = ECommand.TYPE_TURN;
    }
}

/// <summary>
/// 使用技能
/// </summary>
public class USCommand: ICommand
{
    public ESkillPos Pos = ESkillPos.Skill_0;
    public SkillTree Tree = null;
    public float LastTime;

    public USCommand(ESkillPos pos)
    {
        this.Pos = pos;
        this.Command = ECommand.TYPE_USESKILL;
    }
}

/// <summary>
/// 使用技能
/// </summary>
public class JPCommand : ICommand
{

    public JPCommand()
    {
        this.Command = ECommand.TYPE_JUMP;
    }
}



/// <summary>
/// 场景加载
/// </summary>
public class GLCommand:ICommand
{
    public int SceneID;

    public GLCommand(int sceneID)
    {
        this.SceneID = sceneID;
        this.Command = ECommand.TYPE_LOADING;
    }
}


/// <summary>
/// 交谈
/// </summary>
public class TKCommand : ICommand
{
    public string Word;
    public Vector3 Rotation;

    public TKCommand(string word, Vector3 rot)
    {
        this.Word = word;
        this.Rotation = rot;
        this.Command = ECommand.TYPE_TALK;
    }
}



/// <summary>
/// 死亡
/// </summary>
public class DECommand:ICommand
{
    public EDeadReason Reason;

    public DECommand(EDeadReason reason)
    {
        this.Reason = reason;
        this.Command = ECommand.TYPE_DEAD;
    }
}





/// <summary>
/// 冰冻
/// </summary>
public class FSCommand:ICommand
{
    public float LastTime;

    public FSCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_FROST;
    }
}

/// <summary>
/// 昏迷
/// </summary>
public class HMCommand:ICommand
{
    public float LastTime;

    public HMCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_STUN;
    }
}


/// <summary>
/// 恐惧
/// </summary>
public class FRCommand:ICommand
{
    public float LastTime;

    public FRCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_FEAR;
    }
}

/// <summary>
/// 受击
/// </summary>
public class WDCommand:ICommand
{
    public float LastTime;

    public WDCommand()
    {
        this.Command = ECommand.TYPE_WOUND;
    }
}


/// <summary>
/// 被击退
/// </summary>
public class BBCommand:ICommand
{
    public float LastTime;

    public BBCommand()
    {
        this.Command = ECommand.TYPE_BEATBACK;
    }
}


/// <summary>
/// 被击飞
/// </summary>
public class BFCommand:ICommand
{
    public float LastTime;

    public BFCommand()
    {
        this.Command = ECommand.TYPE_BEATFLY;
    }
}




/// <summary>
/// 被击倒
/// </summary>
public class BDCommand : ICommand
{
    public float LastTime;

    public BDCommand()
    {
        this.Command = ECommand.TYPE_BEATDOWN;
    }
}


/// <summary>
/// 被抓取
/// </summary>
public class GBCommand:ICommand
{
    public float LastTime;

    public GBCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_GRAB;
    }
}

/// <summary>
/// 被勾取
/// </summary>
public class HKCommand:ICommand
{
    public float LastTime;

    public HKCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_HOOK;
    }
}

/// <summary>
/// 浮空
/// </summary>
public class FLCommand:ICommand
{
    public float LastTime;

    public FLCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_FLOAT;
    }
}


/// <summary>
/// 滚地
/// </summary>
public class RDCommand:ICommand
{
    public float LastTime;

    public RDCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_ROLL;
    }
}


/// <summary>
/// 变身
/// </summary>
public class VACommand:ICommand
{
    public float LastTime;
    public Int32 ChangeModelID;

    public VACommand(float lastTime,int changeModelID)
    {
        this.LastTime = lastTime;
        this.ChangeModelID = changeModelID;
        this.Command = ECommand.TYPE_VARIATION;
    }
}



/// <summary>
/// 麻痹
/// </summary>
public class MBCommand:ICommand
{
    public float LastTime;

    public MBCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_PALSY;
    }
}


//睡眠
public class SPCommand:ICommand
{
    public float LastTime;

    public SPCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_SLEEP;
    }
}


/// <summary>
/// 定身
/// </summary>
public class FBCommand:ICommand
{
    public float LastTime;

    public FBCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_FIXBODY;
    }
}

//致盲
public class ZMCommand:ICommand
{
    public float LastTime;

    public ZMCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_BLIND;
    }
}

//骑坐骑
public class ERCommand:ICommand
{
    public ERCommand()
    {
        this.Command = ECommand.TYPE_RIDE;
    }
}

//隐身
public class YSCommand:ICommand
{
    public float LastTime;

    public YSCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_STEALTH;
    }
}

//沉默
public class CMCommand:ICommand
{
    public float LastTime;

    public CMCommand(float lastTime)
    {
        this.LastTime = lastTime;
        this.Command = ECommand.TYPE_SILENT;
    }
}

//重生
public class RBCommand:ICommand
{
    public float LastTime;

    public RBCommand()
    {
        this.Command = ECommand.TYPE_REBORN;
    }
}

//采集 
public class CJCommand:ICommand
{
    public float  LastTime;
    public Callback OnFinish;

    public CJCommand(Callback pCallback)
    {
        this.Command = ECommand.TYPE_MINE;
        this.OnFinish = pCallback;
    }
}

//NPC交互
public class ITCommand:ICommand
{
    public float LastTime;
    public string AnimName = "idle";
    public Callback OnFinish;

    public ITCommand(string pAnimName, Callback pCallback)
    {
        this.Command = ECommand.TYPE_INTERACTIVE;
        this.OnFinish = pCallback;
        if (string.IsNullOrEmpty(pAnimName) == false)
        {
            AnimName = pAnimName;
        }
    }
}

*/
