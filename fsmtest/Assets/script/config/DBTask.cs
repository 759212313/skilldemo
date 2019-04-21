using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ETaskType
{
    NONE,
    THREAD=1,   //主线任务
    BRANCH=2,   //支线任务
    DAILY =3,   //日常任务
}

public enum ETaskTargetType
{
   TYPE_NONE,
   TYPE_KILL_COPYBOSS         =1,//击杀副本BOSS
   TYPE_MAIN_PASSCOPY         =2,//通关副本（次数）
   TYPE_UPEQUIP               =3,//升级装备
   TYPE_UPPET                 =4,//升级宠物
   TYPE_UPGEM                 =5,//升级星石
   TYPE_UPPARTNER             =6,//升级伙伴
   TYPE_UPSKILL               =7,//升级角色技能
   TYPE_TALK                  =8,//对话
   TYPE_ROB_TREASURE          =9,//夺宝
   TYPE_AREAE                 =10,//竞技场战斗
   TYPE_PASS_ELITECOPY        =11,//通关精英副本
   TYPE_CHARGE_RELICE         =12,//神器充能
   TYPE_EQUIPSTAR             =13,//装备星级
   TYPE_XHJJC                 =14,//虚幻竞技场
   TYPE_KILLRACE              =15,//杀死种族怪物
}


public enum ETaskCycleType
{
    TYPE_NONE,    
    TYPE_DAILY,   //每日重置
    TYPE_WEEKLY,  //每周重置
    TYPE_SCENE,   //副本重置
}

public enum ETaskState
{
    QUEST_NONE,           //无类型
    QUEST_DOING,          //正在进行任务
    QUEST_CANSUBMIT,      //可提交
    QUEST_FAILED,         //任务失败
    QUEST_HASSUBMIT,      //已经提交
}

public class DBTask:DBModule
{
    public int Id;
    public string Name = string.Empty;
    public string Desc = string.Empty;
    public string Icon = string.Empty;
    public ETaskType Type;
    public ETaskCycleType Cycle;
    public ETaskTargetType TargetType;
    public string TargetArgs = string.Empty;
    public int MinRquireLevel;
    public int MaxRquireLevel;
    public int Condition;
    public int RewardMoneyNum;
    public int RewardExpNum;
    public int    AwardID;
    public string Script;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBTask : IReadConfig<int, DBTask>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBTask> dict)
    {
        DBTask db = new DBTask();
        db.Id = query.GetInt("Id");
        db.Name = query.GetString("Name");
        db.Desc = query.GetString("Desc");
        db.Type = (ETaskType)query.GetInt("Type");
        db.Cycle = (ETaskCycleType)query.GetInt("Cycle");
        db.TargetType = (ETaskTargetType)query.GetInt("TargetType");
        db.TargetArgs = query.GetString("TargetArgs");
        db.Script = query.GetString("Script");
        db.Condition = query.GetInt("Condition");
        db.MinRquireLevel = query.GetInt("MinRquireLevel");
        db.MaxRquireLevel = query.GetInt("MaxRquireLevel");
        db.RewardMoneyNum = query.GetInt("RewardMoneyNum");
        db.RewardExpNum = query.GetInt("RewardExpNum");
        db.AwardID = query.GetInt("AwardID");
        db.Desc = query.GetString("Desc");
        db.Icon = query.GetString("Icon");
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}