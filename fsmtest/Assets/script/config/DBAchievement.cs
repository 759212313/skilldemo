using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//成就
public enum EAchievementType
{
    TYPE_NULL = -1,
    TYPE_TASK = 0,
    TYPE_GROWUP = 1,
    TYPE_LOGIN = 2,
    TYPE_WEALTH = 3,
    TYPE_FIGHT = 4,
    TYPE_EQUIPE = 5,
    TYPE_ECTYPE = 6,
    TYPE_PVP = 7,
    TYPE_OTHER = 8,
    TYPE_SOCIAL = 9,
    TYPE_BOSS = 10,
    TYPE_TITLE_ARENA = 11,
    TYPE_TITLE_TEAMFIGHT = 12
}

public enum EAchivementStatus
{
    STATE_NULL,
    STATE_NOTFINISH,//未完成
    STATE_HASFINISH,//已完成
    STATE_HASREWARD,//已领取奖励
}

public class DBAchievement
{
    public int Id;
    public string Name;
    public EAchievementType AchievementType;
    public int RequireNum;
    public int OpenLevel;
    public int AwardID;
    public int GetAchievement;
    public int GotoID;
}