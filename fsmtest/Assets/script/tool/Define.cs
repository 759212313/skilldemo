using System;
using System.Collections.Generic;



public class Define
{
    public const string Data_Moneys = "Moneys.xml";
    public const string Data_Actions = "Actions.xml";
    public const string Data_Roles = "Roles.xml";
    public const string Data_Gems = "Gems.xml";
    public const string Data_Equips = "Equips.xml";
    public const string Data_Runes = "Runes.xml";
    public const string Data_BagGems  = "BagGems.xml";
    public const string Data_BagItems = "BagItems.xml";
    public const string Data_BagChips = "BagChips.xml";
    public const string Data_BagFashions = "BagFashions.xml";
    public const string Data_BagRunes = "BagRunes.xml";
    public const string Data_DressEquips = "DressEquips.xml";
    public const string Data_DressGems = "DressGems.xml";
    public const string Data_DressRunes = "DressRunes.xml";
    public const string Data_DressFashions = "DressFashions.xml";
    public const string Data_Souls = "Souls.xml";
    public const string Data_Pets = "Pets.xml";
    public const string Data_Partners = "Partners.xml";
    public const string Data_Copys = "Copys.xml";
    public const string Data_MainChapters = "MainChapters.xml";
    public const string Data_Raid = "Raid.xml";
    public const string Data_Mounts = "Mounts.xml";
    public const string Data_Relics = "Relics.xml";
    public const string Data_Guides = "Guides.xml";
    public const string Data_MainTasks = "MainTasks.xml";
    public const string Data_BranchTasks = "BranchTasks.xml";
    public const string Data_DailyTasks = "DailyTasks.xml";
    public const string Data_OfferTasks = "OfferTasks.xml";

    public const string BOARDPATH_PLAYER = "Guis/Board/UIBoardPlayer";
    public const string BOARDPATH_MONSTER = "Guis/Board/UIBoardMonster";
    public const string BOARDPATH_BOSS = "Guis/Board/UIBoardBoss";
    public const string BOARDPATH_NPC = "Guis/Board/UIBoardNPC";
    public const string BOARDPATH_PET = "Guis/Board/UIBoardPet";
    public const string BOARDPATH_PORTAL = "Guis/Board/UIBoardPortal";

    public const int EQUIP_STRENGTHEN_MONEY_ID_1 = 1;
    public const int EQUIP_STRENGTHEN_MONEY_ID_2 = 3;
    public const int GEM_STRENGTHEN_ITEM_ID_1 = 1031;
    public const int GEM_STRENGTHEN_ITEM_ID_2 = 1035;
    public const int GEM_STRENGTHEN_ITEM_ID_3 = 1033;
    public const int GEM_STRENGTHEN_ITEM_ID_4 = 1034;
    public const int GEM_STRENGTHEN_ITEM_ID_5 = 1035;

    public const int PET_UP_ITEM_ID1 = 1036;
    public const int PET_UP_ITEM_ID2 = 1037;
    public const int PET_UP_ITEM_ID3 = 1038;

    public const int LAYER_UI = 5;
    public const int LAYER_PLAYER = 8;
    public const int LAYER_MONSTER = 9;
    public const int LAYER_NPC = 10;
    public const int LAYER_BARRER = 11;
    public const int LAYER_EFFECT = 12;
    public const int LAYER_AVATAR = 13;
    public const int LAYER_PARTNER = 14;
    public const int LAYER_PET = 15;
    public const int LAYER_MOUNT = 16;
    public const int LAYER_TRIGGER = 17;
    public const int LAYER_TOUCHEFFECT = 18;
    public const int LAYER_MINE = 19;

    public const int LAYER_RENDER_START = 25;

    public const int DEPTH_CAM_MAIN = 0;
    public const int DEPTH_CAM_2DUICAMERA = 6;


    public const string BARRIER_PREFAB = "Model/Other/Barrier";
    public const string PORTAL_EFFECT = "Model/Other/PortalEffect";

    public const Single BARRIER_WIDTH = 14;
     
    public const string BT_JUDGE_LIST = "JudgeList";
    public const string BT_JUDGE_SUCCESS = "JudgeSuccess";

    public const Single MIN_INTERVAL_FINDENEMY = 0.5f;
    public const Single MIN_INTERVAL_VALUE     = 0.001f;
    public const Single DAMAGE_RATIO = 1f;

    public const int SCENE_MAP_START = 10001;
    public const int SCENE_LAUNCHER = 10009;
    public const int SCENE_LOGIN = 10010;
    public const int SCENE_Role = 10011;
    public const int SCENE_CITY_1 = 10012;
    public const int SCENE_CITY_2 = 10013;
    public const int SCENE_CITY_3 = 10014;
    public const int SCENE_CITY_4 = 10015;

    public const int WORLD_START_CHAPTER_ID = 1;
    public const int WORLD_END_CHAPTER_ID = 13;

    public const string LOCAL_STRING = "Text/Local/LocalString";

    public const int RECOVER_COST_ITEM_ID = 2;
    public const int RECOVER_COST_ITEM_NUM = 50;

    public const int EFFECT_UPGRADE_ID= 65006;

    public const string SOUND_BATTLE_WIN =  "Sound/Sound/sound_battle_win";
    public const string SOUND_BATTLE_FAIL = "Sound/Sound/sound_battle_fail";

    public static readonly int[] TALENT_LEVELS = { 10, 20, 30, 40, 50, 60 };

    public const int GUIDE_DATA_KEY = 1;
    public const int GUIDE_MAX_CHAPTER = 1;
    public const int TASK_THREAD_DATA_KEY = 1;

    public static string[,] EQUIP_BONES = new string[8, 2]
    {
        {"NULL" ,"NULL"},
        {"NULL" ,"NULL"},
        {"NULL" ,"NULL"},
        {"NULL" ,"NULL"},
        {"NULL" ,"NULL"},
        {"NULL" ,"NULL"},
        {"NULL" ,"NULL"},
        {"Bip01 Prop1" ,"Bip01 Prop2"},
    };

    public const string FLYWORD= "Guis/Flyword/Flyword";

    //public static Dictionary<EFlyWordType, string> FLYWORDPATHS = new Dictionary<EFlyWordType, string>()
    //{
    //    {EFlyWordType.TYPE_AVATAR_HURT,"Guis/HUD/Flyword1" },
    //    {EFlyWordType.TYPE_AVATAR_CRIT,"Guis/HUD/Flyword2" },
    //    {EFlyWordType.TYPE_AVATAR_HEAL,"Guis/HUD/Flyword3" },
    //    {EFlyWordType.TYPE_ENEMY_HURT,"Guis/HUD/Flyword4" },
    //    {EFlyWordType.TYPE_ENEMY_CRIT,"Guis/HUD/Flyword5" },
    //    {EFlyWordType.TYPE_PARTNER_HURT,"Guis/HUD/Flyword6" },
    //    {EFlyWordType.TYPE_PARTNER_CRIT,"Guis/HUD/Flyword7" },
    //};

    public static Dictionary<EBoardType, string> BOARDPATHS = new Dictionary<EBoardType, string>()
    {
        {EBoardType.TYPE_PLAYER,"Guis/Board/UIBoardPlayer" },
        {EBoardType.TYPE_MONSTER,"Guis/Board/UIBoardMonster" },
        {EBoardType.TYPE_NPC,"Guis/Board/UIBoardNPC"},
        {EBoardType.TYPE_PORTAL,"Guis/Board/UIBoardPortal" },
    };

    public static Dictionary<EFlyWordType, string> FLYWORDPATHS = new Dictionary<EFlyWordType, string>()
    {
        {EFlyWordType.TYPE_AVATAR_HURT,"Guis/HUD/Flyword1" },
        {EFlyWordType.TYPE_AVATAR_CRIT,"Guis/HUD/Flyword2" },
        {EFlyWordType.TYPE_AVATAR_HEAL,"Guis/HUD/Flyword3" },
        {EFlyWordType.TYPE_ENEMY_HURT,"Guis/HUD/Flyword4" },
        {EFlyWordType.TYPE_ENEMY_CRIT,"Guis/HUD/Flyword5" },
        {EFlyWordType.TYPE_PARTNER_HURT,"Guis/HUD/Flyword6" },
        {EFlyWordType.TYPE_PARTNER_CRIT,"Guis/HUD/Flyword7" },
    };

    public static bool USE_NEW_SKILL = false;
}
