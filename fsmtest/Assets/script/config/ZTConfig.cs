using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class ZTConfig :Singleton<ZTConfig>
{
    public Dictionary<int, DBItem> DictItem;
    public Dictionary<EProperty, DBProperty> DictProperty;
    public Dictionary<int, DBQuality> DictQuality;
    public Dictionary<int, DBHeroLevel> DictHeroLevel;

    public Dictionary<int, DBEquip> DictEquip;
    public Dictionary<int, DBEquipAdvance> DictEquipAdvance;
    public Dictionary<int, DBEquipStreng> DictEquipStreng;
    public Dictionary<int, Dictionary<int, DBEquipAdvanceCost>> DictEquipAdvanceCost;
    public Dictionary<int, Dictionary<int, DBEquipStrengLevel>> DictEquipStrengLevel;
    public Dictionary<int, Dictionary<int, DBEquipStar>> DictEquipStar;
    public Dictionary<int, DBEquipSuit> DictEquipSuit;
    public Dictionary<int, DBGem> DictGem;
    public Dictionary<int, DBGemSuit> DictGemSuit;
    public Dictionary<int, Dictionary<int,DBGemLevel>> DictGemLevel;
    public Dictionary<int, DBAward> DictAward;
    public Dictionary<int, DBWorld> DictWorld;
    public Dictionary<int, DBCopy> DictCopy;
    public Dictionary<int, DBScene> DictScene;

    public Dictionary<int, DBEffect> DictEffect;
    public Dictionary<int, DBBuff> DictBuff;
    public Dictionary<int, DBBuffAttr> DictBuffAttr;

    public Dictionary<int, DBRelics> DictRelics;

    public Dictionary<int, DBRace> DictRace;
    public Dictionary<int, DBActorGroup> DictActorGroup;

    public Dictionary<int, Dictionary<int,DBStore>> DictStore;
    public Dictionary<int, DBStoreType> DictStoreType;

    public Dictionary<int, Dictionary<int, DBPetLevel>> DictPetLevel;

    public Dictionary<int, DBPartner> DictPartnerBase;
    public Dictionary<int, DBPartnerLevel> DictPartnerLevel;
    public Dictionary<int, DBPartnerAdvance> DictPartnerAdvance;
    public Dictionary<int, DBPartnerStar> DictPartnerStar;
    public Dictionary<int, DBPartnerWake> DictPartnerWake;
    public Dictionary<int, DBPartnerWash> DictPartnerWash;
    public Dictionary<int, DBPartnerFetter> DictPartnerFetter;

    public Dictionary<int, DBTask> DictTask;
    public Dictionary<int, DBCamera> DictCamera;

    public Dictionary<int, DBEntiny> DictEntiny;
    public Dictionary<int, DBAdventure> DictAdventure;
    public Dictionary<int, DBSkill> DictSkill;
    public Dictionary<int, DBSkillTalent> DictSkillTalent;
    public Dictionary<int, DBMine> DictMine;

    public Dictionary<int, DBEntiny> DictPet = new Dictionary<int, DBEntiny>();
    public Dictionary<int, DBEntiny> DictRole = new Dictionary<int, DBEntiny>();
    public Dictionary<int, DBEntiny> DictPartner = new Dictionary<int, DBEntiny>();
    public Dictionary<int, DBEntiny> DictMachine = new Dictionary<int, DBEntiny>();
    public Dictionary<int, DBEntiny> DictNPC = new Dictionary<int, DBEntiny>();
    public Dictionary<int, DBEntiny> DictMount = new Dictionary<int, DBEntiny>();
    public Dictionary<int, DBItem> DictAction = new Dictionary<int, DBItem>();
    public Dictionary<int, DBItem> DictMoney = new Dictionary<int, DBItem>();

    public override void Init()
    {
        Debug.LogError("===============");

        string path = ZTResource.Instance.LoadFromStreamingAssets("/db.bytes", "/DB/db.bytes");
        SqliteDataBase.OpenDatabase(path);

        DictProperty = new Dictionary<EProperty, DBProperty>();
        new ReadDBProperty().Load("SELECT * FROM Data_Property", DictProperty);

        DictQuality = new Dictionary<int, DBQuality>();
        new ReadDBQuality().Load("SELECT * FROM Data_Quality", DictQuality);

        DictHeroLevel = new Dictionary<int, DBHeroLevel>();
        new ReadDBHeroLevel().Load("SELECT * FROM Data_HeroLevel", DictHeroLevel);

        DictItem = new Dictionary<int, DBItem>();
        new ReadDBItem().Load("SELECT * FROM Data_Item", DictItem);

        DictEquip = new Dictionary<int, DBEquip>();
        new ReadDBEquip().Load("SELECT * FROM Data_Equip", DictEquip);

        DictEquipAdvance = new Dictionary<int, DBEquipAdvance>();
        new ReadDBEquipAdvance().Load("SELECT * FROM Data_EquipAdvance", DictEquipAdvance);

        DictEquipAdvanceCost = new Dictionary<int, Dictionary<int, DBEquipAdvanceCost>>();
        new ReadDBEquipAdvanceCost().Load("SELECT * FROM Data_EquipAdvanceCost", DictEquipAdvanceCost);

        DictEquipStreng = new Dictionary<int, DBEquipStreng>();
        new ReadDBEquipStreng().Load("SELECT * FROM Data_EquipStrengthen", DictEquipStreng);

        DictEquipStrengLevel = new Dictionary<int, Dictionary<int, DBEquipStrengLevel>>();
        new ReadDBEquipStrengLevel().Load("SELECT * FROM Data_EquipStrengthenLevel", DictEquipStrengLevel);

        DictEquipStar = new Dictionary<int, Dictionary<int, DBEquipStar>>();
        new ReadDBEquipStar().Load("SELECT * FROM Data_EquipStar", DictEquipStar);

        DictEquipSuit = new Dictionary<int, DBEquipSuit>();
        new ReadDBEquipSuit().Load("SELECT * FROM Data_EquipSuit", DictEquipSuit);

        DictGem = new Dictionary<int, DBGem>();
        new ReadDBGem().Load("SELECT * FROM Data_Gem", DictGem);

        DictGemLevel = new Dictionary<int, Dictionary<int, DBGemLevel>>();
        new ReadDBGemLevel().Load("SELECT * FROM Data_GemLevel", DictGemLevel);

        DictGemSuit = new Dictionary<int, DBGemSuit>();
        new ReadDBGemSuit().Load("SELECT * FROM Data_GemSuit", DictGemSuit);

        DictAward = new Dictionary<int, DBAward>();
        new ReadDBAward().Load("SELECT * FROM Data_Award", DictAward);

        DictWorld = new Dictionary<int, DBWorld>();
        new ReadDBWorld().Load("SELECT * FROM Data_World", DictWorld);

        DictCopy = new Dictionary<int, DBCopy>();
        new ReadDBCopy().Load("SELECT * FROM Data_Copy", DictCopy);

        DictScene = new Dictionary<int, DBScene>();
        new ReadDBScene().Load("SELECT * FROM Data_Scene", DictScene);

        DictEffect = new Dictionary<int, DBEffect>();
        new ReadDBEffect().Load("SELECT * FROM Data_Effect", DictEffect);

        DictBuff = new Dictionary<int, DBBuff>();
        new ReadDBBuff().Load("SELECT * FROM Data_Buff", DictBuff);

        DictBuffAttr = new Dictionary<int, DBBuffAttr>();
        new ReadDBBuffAttr().Load("SELECT * FROM Data_BuffAttr", DictBuffAttr);

        DictRelics = new Dictionary<int, DBRelics>();
        new ReadDBRelics().Load("SELECT * FROM Data_Relics", DictRelics);

        DictRace = new Dictionary<int, DBRace>();
        new ReadDBRace().Load("SELECT * FROM Data_Race", DictRace);

        DictActorGroup = new Dictionary<int, DBActorGroup>();
        new ReadDBActorGroup().Load("SELECT * FROM Data_ActorGroup", DictActorGroup);

        DictStore = new Dictionary<int, Dictionary<int, DBStore>>();
        new ReadDBStore().Load("SELECT * FROM Data_Store", DictStore);

        DictStoreType = new Dictionary<int, DBStoreType>();
        new ReadDBStoreType().Load("SELECT * FROM Data_StoreType", DictStoreType);


        DictPetLevel = new Dictionary<int, Dictionary<int, DBPetLevel>>();
        new ReadDBPetLevel().Load("SELECT * FROM Data_PetLevel", DictPetLevel);

        DictPartnerBase = new Dictionary<int, DBPartner>();
        new ReadDBPartner().Load("SELECT * FROM Data_Partner", DictPartnerBase);

        DictPartnerLevel = new Dictionary<int, DBPartnerLevel>();
        new ReadDBPartnerLevel().Load("SELECT * FROM Data_PartnerLevel", DictPartnerLevel);

        DictPartnerAdvance = new Dictionary<int, DBPartnerAdvance>();
        new ReadDBPartnerAdvance().Load("SELECT * FROM Data_PartnerAdvance", DictPartnerAdvance);

        DictPartnerWash = new Dictionary<int, DBPartnerWash>();
        new ReadDBPartnerWash().Load("SELECT * FROM Data_PartnerWash", DictPartnerWash);

        DictPartnerWake = new Dictionary<int, DBPartnerWake>();
        new ReadDBPartnerWake().Load("SELECT * FROM Data_PartnerWake", DictPartnerWake);

        DictPartnerFetter = new Dictionary<int, DBPartnerFetter>();
        new ReadDBPartnerFetter().Load("SELECT * FROM Data_PartnerFetter", DictPartnerFetter);

        DictPartnerStar = new Dictionary<int, DBPartnerStar>();
        new ReadDBPartnerStar().Load("SELECT * FROM Data_PartnerStar ", DictPartnerStar);

        DictTask = new Dictionary<int, DBTask>();
        new ReadDBTask().Load("SELECT * FROM Data_Task", DictTask);

        DictCamera = new Dictionary<int, DBCamera>();
        new ReadDBCamera().Load("SELECT * FROM Data_Camera", DictCamera);

        DictEntiny = new Dictionary<int, DBEntiny>();
        new ReadDBEntiny().Load("SELECT * FROM Data_Entiny", DictEntiny);

        DictAdventure = new Dictionary<int, DBAdventure>();
        new ReadDBAdventure().Load("SELECT * FROM Data_Adventure", DictAdventure);

        DictSkill = new Dictionary<int, DBSkill>();
        new ReadDBSkill().Load("SELECT * FROM Data_Skill", DictSkill);

        DictSkillTalent = new Dictionary<int, DBSkillTalent>();
        new ReadDBSkillTalent().Load("SELECT * FROM Data_SkillTalent", DictSkillTalent);

        DictMine = new Dictionary<int, DBMine>();
        new ReadDBMine().Load("SELECT * FROM Data_Mine", DictMine);

        Dictionary<int, DBEntiny>.Enumerator em1 = DictEntiny.GetEnumerator();
        while (em1.MoveNext())
        {
            switch(em1.Current.Value.Type)
            {
                case EActorType.PET:
                    DictPet[em1.Current.Key]= em1.Current.Value;
                    break;
                case EActorType.PARTNER:
                    DictPartner[em1.Current.Key] = em1.Current.Value;
                    break;
                case EActorType.PLAYER:
                    DictRole[em1.Current.Key] = em1.Current.Value;
                    break;
                case EActorType.MOUNT:
                    DictMount[em1.Current.Key] = em1.Current.Value;
                    break;
                case EActorType.NPC:
                    DictNPC[em1.Current.Key] = em1.Current.Value;
                    break;
                case EActorType.MACHINE:
                    DictMachine[em1.Current.Key] = em1.Current.Value;
                    break;
            }
        }
        em1.Dispose();

        Dictionary<int, DBItem>.Enumerator em2 = DictItem.GetEnumerator();
        while(em2.MoveNext())
        {
            switch (em2.Current.Value.ItemType)
            {
                case EItemType.ACTION:
                    DictAction[em2.Current.Key] = em2.Current.Value;
                    break;
                case EItemType.MONEY:
                    DictMoney[em2.Current.Key] = em2.Current.Value;
                    break;
            }
        }
        em2.Dispose();
    }

    public DBItem GetDBMoney(EMoney moneyType)
    { 
        DBItem db = null;
        DictItem.TryGetValue((int)moneyType, out db);
        return db;
    }

    public DBItem GetDBAction(EAction actionType)
    {
        switch(actionType)
        {
            case EAction.Manual:
                return GetDBItem(101);
            case EAction.Energy:
                return GetDBItem(102);
            default:
                return null; 
        }
    }

    public EAction GetActionType(int key)
    {
        switch (key)
        {
            case 101:
                return EAction.Manual;
            case 102:
                return EAction.Energy;
            default:
                return EAction.None;
        }
    }

    public DBQuality GetDBQuality(int quality)
    {
        DBQuality db = null;
        DictQuality.TryGetValue(quality, out db);
        return db;
    }

    public DBProperty GetDBProperty(EProperty property)
    {
        DBProperty db = null;
        DictProperty.TryGetValue(property, out db);
        return db;
    }

    public DBHeroLevel GetDBHeroLevel(int level)
    {
        DBHeroLevel db = null;
        DictHeroLevel.TryGetValue(level, out db);
        return db;
    }

    public DBItem GetDBItem(int id)
    {
        DBItem db = null;
        DictItem.TryGetValue(id, out db);
        return db;
    }

    public DBEquip GetDBEquip(int id)
    {
        DBEquip db = null;
        DictEquip.TryGetValue(id, out db);
        return db;
    }

    public DBEquipAdvance GetDBEquipAdvance(int id)
    {
        DBEquipAdvance db = null;
        DictEquipAdvance.TryGetValue(id, out db);
        return db;
    }

    public DBEquipStreng GetDBEquipStreng(int id)
    {
        DBEquipStreng db = null;
        DictEquipStreng.TryGetValue(id, out db);
        return db;
    }


    public DBEquipAdvanceCost GetDBEquipAdvanceCost(int quality,int advanceLevel)
    {
        Dictionary<int, DBEquipAdvanceCost> dict = null;
        DictEquipAdvanceCost.TryGetValue(quality, out dict);
        if (dict == null)
        {
            return null;
        }
        DBEquipAdvanceCost db = null;
        dict.TryGetValue(advanceLevel, out db);
        return db;
    }

    public DBEquipStrengLevel GetDBEquipStrengLevel(int quality, int level)
    {
        Dictionary<int, DBEquipStrengLevel> dict = null;
        DictEquipStrengLevel.TryGetValue(quality, out dict);
        if (dict == null)
        {
            return null;
        }
        DBEquipStrengLevel db = null;
        dict.TryGetValue(level, out db);
        return db;
    }

    public DBEquipStar GetDBEquipStar(int quality,int starLevel)
    {
        Dictionary<int, DBEquipStar> dict = null;
        DictEquipStar.TryGetValue(quality, out dict);
        if (dict == null)
        {
            return null;
        }
        DBEquipStar db = null;
        dict.TryGetValue(starLevel, out db);
        return db;
    }

    public DBEquipSuit GetDBEquipSuit(int id)
    {
        DBEquipSuit db = null;
        DictEquipSuit.TryGetValue(id, out db);
        return db;
    }

    public DBGem GetDBGem(int id)
    {
        DBGem db = null;
        DictGem.TryGetValue(id, out db);
        return db;
    }

    public DBGemLevel GetDBGemLevel(int quality,int level)
    {
        Dictionary<int, DBGemLevel> dict = null;
        DictGemLevel.TryGetValue(quality, out dict);
        if(dict==null)
        {
            return null;
        }
        DBGemLevel db = null;
        dict.TryGetValue(level, out db);
        return db; 
    }

    public DBGemSuit GetDBGemSuit(int id)
    {
        DBGemSuit db = null;
        DictGemSuit.TryGetValue(id, out db);
        return db;
    }


    public DBAward GetDBAward(int id)
    {
        DBAward db = null;
        DictAward.TryGetValue(id, out db);
        return db;
    }


    public DBWorld GetDBWorld(int id)
    {
        DBWorld db = null;
        DictWorld.TryGetValue(id, out db);
        return db;
    }


    public DBCopy GetDBCopy(int id)
    {
        DBCopy db = null;
        DictCopy.TryGetValue(id, out db);
        return db;
    }


    public DBScene GetDBScene(int id)
    {
        DBScene db = null;
        DictScene.TryGetValue(id, out db);
        return db;
    }

    public DBEffect GetDBEffect(int id)
    {
        DBEffect db = null;
        DictEffect.TryGetValue(id, out db);
        return db;
    }

    public DBBuff GetDBBuff(int id)
    {
        DBBuff db = null;
        DictBuff.TryGetValue(id, out db);
        return db;
    }

    public DBBuffAttr GetDBBuffAttr(int id)
    {
        DBBuffAttr attr = null;
        DictBuffAttr.TryGetValue(id, out attr);
        return attr;
    }

    public DBEntiny GetDBEntiny(int id)
    {
        DBEntiny db = null;
        DictEntiny.TryGetValue(id, out db);
        return db;
    }

    public DBPartner GetDBPartner(int id)
    {
        DBPartner db = null;
        DictPartnerBase.TryGetValue(id, out db);
        return db;
    }

    public DBPartnerLevel GetDBPartnerLevel(int id)
    {
        DBPartnerLevel dict = null;
        DictPartnerLevel.TryGetValue(id, out dict);
        return dict;
    }

    public DBPartnerAdvance GetDBPartnerAdvance(int id)
    {
        DBPartnerAdvance db = null;
        DictPartnerAdvance.TryGetValue(id, out db);
        return db;
    }


    public DBRelics GetDBRelics(int id)
    {
        DBRelics db = null;
        DictRelics.TryGetValue(id, out db);
        return db;
    }

    public DBRace GetDBRace(int id)
    {
        DBRace db = null;
        DictRace.TryGetValue(id, out db);
        return db;
    }

    public DBRace GetDBRace(EActorRace race)
    {
        return GetDBRace((int)race);
    }

    public DBStoreType GetDBStoreType(int id)
    {
        DBStoreType db = null;
        DictStoreType.TryGetValue(id, out db);
        return db;
    }

    public DBStore GetDBStore(int storeType, int id)
    {
        Dictionary<int, DBStore> dict = null;
        DictStore.TryGetValue(storeType, out dict);
        if(dict==null)
        {
            return null;
        }
        DBStore db = null;
        dict.TryGetValue(id, out db);
        return db;
    }

    public DBPetLevel GetDBPetLevel(int quality,int level)
    {
        Dictionary<int, DBPetLevel> dict = null;
        DictPetLevel.TryGetValue(quality, out dict);
        if (dict == null)
        {
            return null;
        }
        DBPetLevel db = null;
        dict.TryGetValue(level, out db);
        return db;
    }

    public Dictionary<int,DBPetLevel> GetDBPetLevels(int quality)
    {
        Dictionary<int, DBPetLevel> dict = null;
        DictPetLevel.TryGetValue(quality, out dict);
        return dict;
    }

    public DBTask GetDBTask(int id)
    {
        DBTask db = null;
        DictTask.TryGetValue(id, out db);
        return db;
    }

    public DBCamera GetDBCamera(int id)
    {
        DBCamera db = null;
        DictCamera.TryGetValue(id, out db);
        return db;
    }

    public DBAdventure GetDBAdventure(int id)
    {
        DBAdventure db = null;
        DictAdventure.TryGetValue(id, out db);
        return db;
    }

    public DBSkill GetDBSkill(int id)
    {
        DBSkill db = null;
        DictSkill.TryGetValue(id, out db);
        return db;
    }

    public DBSkillTalent GetDBSkillTalent(int id)
    {
        DBSkillTalent db = null;
        DictSkillTalent.TryGetValue(id, out db);
        return db;
    }

    public DBMine GetDBMine(int id)
    {
        DBMine db = null;
        DictMine.TryGetValue(id, out db);
        return db;
    }
}
