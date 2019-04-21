using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


public class DataManager : Singleton<DataManager>
{
    private int  mMaxInstance= PlayerPrefs.GetInt("MaxInstance", 1001);
    private int  mCurRoleID = 1;

    private long mLastLoginTime;
    private long mGameStartTime;
    private List<int>  mActionKeyList = new List<int>();
    private List<long> mNextActionTimes = new List<long>();

    public int MaxInstance
    {
        get { return mMaxInstance; }
        set
        {
            mMaxInstance = value;
            PlayerPrefs.SetInt("MaxInstance", mMaxInstance);
        }
    }

    public long LastLoginTime
    {
        get { return mLastLoginTime; }
        set
        {
            mLastLoginTime = value;
            PlayerPrefs.SetString("LastLoginTime", value.ToString());
        }
    }

    public long CurServerTime
    {
        get { return GTTools.GetUtcTime() - mGameStartTime; }
    }

    public long GameStartTime
    {
        get { return mGameStartTime; }
    }

    public int CurRoleID
    {
        get { return mCurRoleID; }
        set { mCurRoleID = value; }
    }

    public void LoadSystemInfo()
    {
        mLastLoginTime = long.Parse(PlayerPrefs.GetString("LastLoginTime", GTTools.GetUtcTime().ToString()));
        mGameStartTime = GTTools.GetUtcTime();
        ActionStart();
        if(mGameStartTime-mLastLoginTime>=21600)
        {
            DataDailyTask.ClearAll();
        }
    }

    public void SaveSystemInfo()
    {
        LastLoginTime = GTTools.GetUtcTime();
    }

    public Dictionary<int, XRole> Roles = new Dictionary<int, XRole>();
    public Dictionary<int, XMoney> Moneys = new Dictionary<int, XMoney>();
    public Dictionary<int, XAction> Actions = new Dictionary<int, XAction>();
    public Dictionary<int, XSoul> Souls = new Dictionary<int, XSoul>();

    public Dictionary<int, XItem> BagItems = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> BagGems = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> BagChips = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> BagRunes = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> BagFashions = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> DressEquips = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> DressGems = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> DressRunes = new Dictionary<int, XItem>();
    public Dictionary<int, XItem> DressFashions = new Dictionary<int, XItem>();

    public Dictionary<int, XEquip> Equips = new Dictionary<int, XEquip>();
    public Dictionary<int, XGem> Gems = new Dictionary<int, XGem>();
    public Dictionary<int, XRune> Runes = new Dictionary<int, XRune>();

    public Dictionary<int, XMainChapter> MainChapters = new Dictionary<int, XMainChapter>();
    public Dictionary<int, XCopy> Copys = new Dictionary<int, XCopy>();
    public Dictionary<int, XRaid> Raids = new Dictionary<int, XRaid>();

    public Dictionary<int, XPartner> Partners = new Dictionary<int, XPartner>();
    public Dictionary<int, XMount> Mounts = new Dictionary<int, XMount>();
    public Dictionary<int, XRelics> Relics = new Dictionary<int, XRelics>();

    public Dictionary<int, XPet> Pets = new Dictionary<int, XPet>();
    public Dictionary<int, XGuide> Guides = new Dictionary<int, XGuide>();


    public Dictionary<int, XThreadTask> ThreadTasks = new Dictionary<int, XThreadTask>();
    public Dictionary<int, XBranchTask> BranchTasks = new Dictionary<int, XBranchTask>();
    public Dictionary<int, XDailyTask> DailyTasks = new Dictionary<int, XDailyTask>();

    public DataReadRole DataRole { get; private set; }
    public DataReadMoney DataMoney { get; private set; }
    public DataReadAction DataAction { get; private set; }
    public DataReadSoul DataSoul { get; private set; }

    public DataReadBagItem DataBagItem { get; private set; }
    public DataReadBagGem DataBagGem { get; private set; }
    public DataReadBagChip DataBagChip { get; private set; }
    public DataReadBagRune DataBagRune { get; private set; }
    public DataReadBagFashion DataBagFashion { get; private set; }

    public DataReadDressEquip DataDressEquip { get; private set; }
    public DataReadDressGem DataDressGem { get; private set; }
    public DataReadDressRune DataDressRune { get; private set; }
    public DataReadDressFashion DataDressFashion { get; private set; }
    public DataReadGem DataGem { get; private set; }
    public DataReadEquip DataEquip { get; private set; }
    public DataReadRune DataRune { get; private set; }

    public DataReadMainChapter DataMainChapter { get; private set; }
    public DataReadRaid DataRaid { get; private set; }
    public DataReadCopy DataCopy { get; private set; }

    public DataReadPartner DataPartner { get; private set; }
    public DataReadRelics DataRelics { get; private set; }
    public DataReadMount DataMount { get; private set; }
    public DataReadPet DataPet { get; private set; }
    public DataReadGuide DataGuide { get; private set; }

    public DataReadThreadTask DataThreadTask { get; private set; }
    public DataReadBranchTask DataBranchTask { get; private set; }
    public DataReadDailyTask DataDailyTask { get; private set; }

    public string GetDataPath(string xmlName)
    {
        if(mCurRoleID==0)
        {
            mCurRoleID = 1;
        }
        string pPath = string.Format("{0}/Data/{1}", ZTResource.Instance.GetExtPath(),mCurRoleID);
        if (!Directory.Exists(pPath))
        {
            Directory.CreateDirectory(pPath);
        }
        string path = string.Format("{0}/{1}", pPath, xmlName);
        return path;
    }

    public string GetCommonDataPath(string xmlName)
    {
        string pPath = string.Format("{0}/Data", ZTResource.Instance.GetExtPath());
        if (!Directory.Exists(pPath))
        {
            Directory.CreateDirectory(pPath);
        }
        string path = string.Format("{0}/{1}", pPath, xmlName);
        return path;
    }

    public void LoadCommonData()
    {
        DataRole = new DataReadRole();
        DataRole.xmlPath = GetCommonDataPath(Define.Data_Roles);
        DataRole.keyType = DataKeyType.Id;
        DataRole.dict = Roles;
        ReadData(DataRole);
    }

    public void Clear()
    {
        Moneys.Clear();
        Actions.Clear();
        Souls.Clear();

        BagItems.Clear();
        BagGems.Clear();
        BagChips.Clear();
        BagRunes.Clear();
        BagFashions.Clear();

        DressEquips.Clear();
        DressFashions.Clear();
        DressGems.Clear();
        DressRunes.Clear();

        Equips.Clear();
        Gems.Clear();
        Runes.Clear();
        MainChapters.Clear();
        Copys.Clear();
        Raids.Clear();

        Relics.Clear();
        Mounts.Clear();
        Partners.Clear();
        Pets.Clear();
        Guides.Clear();

        ThreadTasks.Clear();
        BranchTasks.Clear();
        DailyTasks.Clear();
    }

    public void LoadData()
    {
        Clear();
        DataBagItem = new DataReadBagItem();
        DataBagItem.xmlPath = GetDataPath(Define.Data_BagItems);
        DataBagItem.keyType = DataKeyType.Pos;
        DataBagItem.dict = BagItems;
        ReadData(DataBagItem);

        DataEquip = new DataReadEquip();
        DataEquip.xmlPath = GetDataPath(Define.Data_Equips);
        DataEquip.keyType = DataKeyType.Instance;
        DataEquip.dict = Equips;
        ReadData(DataEquip);

        DataDressEquip = new DataReadDressEquip();
        DataDressEquip.xmlPath = GetDataPath(Define.Data_DressEquips);
        DataDressEquip.keyType = DataKeyType.Pos;
        DataDressEquip.dict = DressEquips;
        ReadData(DataDressEquip);


        DataMoney = new DataReadMoney();
        DataMoney.xmlPath = GetDataPath(Define.Data_Moneys);
        DataMoney.keyType = DataKeyType.Id;
        DataMoney.dict = Moneys;
        ReadData(DataMoney);

        DataAction = new DataReadAction();
        DataAction.xmlPath = GetDataPath(Define.Data_Actions);
        DataAction.keyType = DataKeyType.Id;
        DataAction.dict = Actions;
        ReadData(DataAction);

        DataSoul = new DataReadSoul();
        DataSoul.xmlPath = GetDataPath(Define.Data_Souls);
        DataSoul.keyType = DataKeyType.Id;
        DataSoul.dict = Souls;
        ReadData(DataSoul);

        DataBagGem = new DataReadBagGem();
        DataBagGem.xmlPath = GetDataPath(Define.Data_BagGems);
        DataBagGem.keyType = DataKeyType.Pos;
        DataBagGem.dict = BagGems;
        ReadData(DataBagGem);

        DataGem = new DataReadGem();
        DataGem.xmlPath = GetDataPath(Define.Data_Gems);
        DataGem.keyType = DataKeyType.Instance;
        DataGem.dict = Gems;
        ReadData(DataGem);

        DataDressGem = new DataReadDressGem();
        DataDressGem.xmlPath = GetDataPath(Define.Data_DressGems);
        DataDressGem.keyType = DataKeyType.Pos;
        DataDressGem.dict = DressGems;
        ReadData(DataDressGem);


        DataBagChip = new DataReadBagChip();
        DataBagChip.xmlPath = GetDataPath(Define.Data_BagChips);
        DataBagChip.keyType = DataKeyType.Pos;
        DataBagChip.dict = BagChips;
        ReadData(DataBagChip);

        DataBagFashion = new DataReadBagFashion();
        DataBagFashion.xmlPath = GetDataPath(Define.Data_BagFashions);
        DataBagFashion.keyType = DataKeyType.Pos;
        DataBagFashion.dict = BagFashions;
        ReadData(DataBagFashion);

        DataBagRune = new DataReadBagRune();
        DataBagRune.xmlPath = GetDataPath(Define.Data_BagRunes);
        DataBagRune.keyType = DataKeyType.Pos;
        DataBagRune.dict = BagRunes;
        ReadData(DataBagRune);

        DataDressRune = new DataReadDressRune();
        DataDressRune.xmlPath = GetDataPath(Define.Data_DressRunes);
        DataDressRune.keyType = DataKeyType.Pos;
        DataDressRune.dict = DressRunes;
        ReadData(DataDressRune);

        DataDressFashion = new DataReadDressFashion();
        DataDressFashion.xmlPath = GetDataPath(Define.Data_DressFashions);
        DataDressFashion.keyType = DataKeyType.Pos;
        DataDressFashion.dict = DressFashions;
        ReadData(DataDressFashion);

        DataRune = new DataReadRune();
        DataRune.xmlPath = GetDataPath(Define.Data_Runes);
        DataRune.keyType = DataKeyType.Instance;
        DataRune.dict = Runes;
        ReadData(DataRune);


        DataCopy = new DataReadCopy();
        DataCopy.xmlPath = GetDataPath(Define.Data_Copys);
        DataCopy.keyType = DataKeyType.Id;
        DataCopy.dict = Copys;
        ReadData(DataCopy);

        DataMainChapter = new DataReadMainChapter();
        DataMainChapter.xmlPath = GetDataPath(Define.Data_MainChapters);
        DataMainChapter.keyType = DataKeyType.Id;
        DataMainChapter.dict = MainChapters;
        ReadData(DataMainChapter);

        DataRaid = new DataReadRaid();
        DataRaid.xmlPath = GetDataPath(Define.Data_Raid);
        DataRaid.keyType = DataKeyType.Id;
        DataRaid.dict = Raids;
        ReadData(DataRaid);

        DataPartner = new DataReadPartner();
        DataPartner.xmlPath = GetDataPath(Define.Data_Partners);
        DataPartner.keyType = DataKeyType.Id;
        DataPartner.dict = Partners;
        ReadData(DataPartner);

        DataMount = new DataReadMount();
        DataMount.xmlPath = GetDataPath(Define.Data_Mounts);
        DataMount.keyType = DataKeyType.Id;
        DataMount.dict = Mounts;
        ReadData(DataMount);

        DataRelics = new DataReadRelics();
        DataRelics.xmlPath = GetDataPath(Define.Data_Relics);
        DataRelics.keyType = DataKeyType.Id;
        DataRelics.dict = Relics;
        ReadData(DataRelics);

        DataPet = new DataReadPet();
        DataPet.xmlPath = GetDataPath(Define.Data_Pets);
        DataPet.keyType = DataKeyType.Id;
        DataPet.dict = Pets;
        ReadData(DataPet);

        DataGuide = new DataReadGuide();
        DataGuide.xmlPath = GetDataPath(Define.Data_Guides);
        DataGuide.keyType = DataKeyType.Id;
        DataGuide.dict = Guides;
        ReadData(DataGuide);

        DataThreadTask = new DataReadThreadTask();
        DataThreadTask.xmlPath = GetDataPath(Define.Data_MainTasks);
        DataThreadTask.keyType = DataKeyType.Id;
        DataThreadTask.dict = ThreadTasks;
        ReadData(DataThreadTask);

        DataBranchTask = new DataReadBranchTask();
        DataBranchTask.xmlPath = GetDataPath(Define.Data_BranchTasks);
        DataBranchTask.keyType = DataKeyType.Id;
        DataBranchTask.dict = BranchTasks;
        ReadData(DataThreadTask);

        DataDailyTask = new DataReadDailyTask();
        DataDailyTask.xmlPath = GetDataPath(Define.Data_DailyTasks);
        DataDailyTask.keyType = DataKeyType.Id;
        DataDailyTask.dict = DailyTasks;
        ReadData(DataDailyTask);

        LoadSystemInfo();
    }


    public void ReadData<T>(DataReadBase<T> dataRead) where T :XModule,new ()
    {
        XmlNodeList nodeList = EXml.GetXmlNodeList(dataRead.xmlPath);
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement xe = nodeList.Item(i) as XmlElement;
            if (xe == null)
            {
                continue;
            }
            int key = xe.GetAttribute(dataRead.keyType.ToString()).ToInt32();
            for (int j = 0; j < xe.Attributes.Count; j++)
            {
                string name = xe.Attributes[j].Name;
                string value = xe.Attributes[j].Value;
                dataRead.AppendAttribute(key, name, value);
            }
        }
    }


    public void ReadDataExtend<T>(DataReadBase<T> dataRead) where T : XModule, new()
    {
        XmlNodeList nodeList = EXml.GetXmlNodeList(dataRead.xmlPath);
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlAttribute xa = (nodeList[i] as XmlElement).GetAttributeNode(dataRead.keyType.ToString());
            if (xa == null)
            {
                continue;
            }
            int key = Convert.ToInt32(xa.InnerText);
            for(int k=0;k<xa.ChildNodes.Count;i++)
            {
                XmlElement xe = (XmlElement)xa.ChildNodes[i];
                string name = xe.Name;
                string value = xe.Value;
                dataRead.AppendAttribute(key,name, value);
            }
        }
    }

    public Dictionary<int, XItem> GetItemDataByPosType(EPosType posType)
    {
        switch (posType)
        {
            case EPosType.BagItem:
                return BagItems;
            case EPosType.BagChip:
                return BagChips;
            case EPosType.BagFashion:
                return BagFashions;
            case EPosType.BagGem:
                return BagGems;
            case EPosType.BagRune:
                return BagRunes;
            case EPosType.RoleEquip:
                return DressEquips;
            case EPosType.RoleFashion:
                return DressFashions;
            case EPosType.RoleGem:
                return DressGems;
            case EPosType.RoleRune:
                return DressRunes;
            default:
                return new Dictionary<int, XItem>();
        }
    }

    public Dictionary<int, XItem> GetBagDataByBagType(EBagType bagType)
    {
        switch (bagType)
        {
            case EBagType.ITEM:
                return BagItems;
            case EBagType.CHIP:
                return BagChips;
            case EBagType.FASHION:
                return BagFashions;
            case EBagType.GEM:
                return BagGems;
            case EBagType.RUNE:
                return BagRunes;
            default:
                return new Dictionary<int, XItem>();
        }
    }

    public XItem GetItemDataById(int id)
    {
        DBItem itemDB = ZTConfig.Instance.GetDBItem(id);
        Dictionary<int, XItem> items = GetBagDataByBagType(itemDB.BagType);
        XItem item = null;
        foreach (KeyValuePair<int, XItem> pair in items)
        {
            if (pair.Value.Id == id)
            {
                item = pair.Value;
            }
        }
        return item;
    }

    public int GetItemCountById(int id)
    {
        DBItem db = ZTConfig.Instance.GetDBItem(id);
        if (db == null) return 0;
        int value = 0;
        switch (db.ItemType)
        {
            case EItemType.MONEY:
                {
                    XMoney data = DataMoney.GetDataById(id);
                    value = data == null ? 0 : data.Num;
                }
                break;
            case EItemType.ACTION:
                {
                    XAction data = DataAction.GetDataById(id);
                    value = data == null ? 0 : data.Num;
                }
                break;
            case EItemType.PETSOUL:
                {
                    XSoul data = DataSoul.GetDataById(id);
                    value = data == null ? 0 : data.Num;
                }
                break;
            case EItemType.BOX:
            case EItemType.KEY:
            case EItemType.DRUG:
            case EItemType.MAT:
                foreach (KeyValuePair<int, XItem> pair in BagItems)
                {
                    if (pair.Value.Id == id)
                    {
                        value = pair.Value.Num;
                    }
                }
                break;
            case EItemType.CHIP:
                foreach (KeyValuePair<int, XItem> pair in BagChips)
                {
                    if (pair.Value.Id == id)
                    {
                        value = pair.Value.Num;
                    }
                }
                break;
            default:
                break;
        }
        return value;
    }

    public int GetActionCountByType(EAction actionType)
    {
        switch (actionType)
        {
            case EAction.Manual:
                return GetItemCountById(101);
            case EAction.Energy:
                return GetItemCountById(102);
            default:
                return 0;
        }
    }

    public XEquip GetXEquipByPos(EPosType posType, int pos)
    {
        Dictionary<int, XItem> dict = GetItemDataByPosType(posType);
        if (!dict.ContainsKey(pos)) return null;
        XItem itemPos = dict[pos];
        XEquip equip = null;
        if (Equips.ContainsKey(itemPos.Instance))
        {
            equip = Equips[itemPos.Instance];
        }
        return equip;
    }

    public XGem GetXGemByPos(EPosType posType, int pos)
    {
        Dictionary<int, XItem> dict = GetItemDataByPosType(posType);
        if (!dict.ContainsKey(pos)) return null;
        XItem itemPos = dict[pos];
        XGem gem = null;
        if (Gems.ContainsKey(itemPos.Instance))
        {
            gem = Gems[itemPos.Instance];
        }
        return gem;
    }

    public XRole GetCurRole()
    {
        XRole role = null;
        Roles.TryGetValue(mCurRoleID, out role);
        return role;
    }

    public int GetBagNum()
    {
        return 120;
    }

    public int GetNewPos(EBagType bagType)
    {
        Dictionary<int, XItem> itemData =GetBagDataByBagType(bagType);
        if (itemData == null) return 0;
        int newPos = 0;
        for (int i = 1; i <= GetBagNum(); i++)
        {
            if (!itemData.ContainsKey(i))
            {
                newPos = i;
                break;
            }
        }
        return newPos;
    }

    public void AddNewItem(int id, int num)
    {

        if (num < 1)
        {
            return;
        }
        if (!ZTConfig.Instance.DictItem.ContainsKey(id))
        {
            return;
        }
        DBItem db = ZTConfig.Instance.GetDBItem(id);
        switch (db.ItemType)
        {
            case EItemType.EQUIP:
                for (int i = 0; i < num; i++)
                {
                    MaxInstance++;
                    AddNewEquip(MaxInstance, id);
                }
                break;
            case EItemType.GEM:
                for (int i = 0; i < num; i++)
                {
                    MaxInstance++;
                    AddNewGem(MaxInstance, id);
                }
                break;
            case EItemType.MONEY:
                AddMoney(id, num);
                break;
            case EItemType.ACTION:
                AddAction(id, num);
                break;
            case EItemType.PETSOUL:
                AddSoul(id, num);
                break;
            case EItemType.FASHION:
                for (int i = 0; i < num; i++)
                {
                    DataManager.Instance.MaxInstance++;
                    AddNewFashion(DataManager.Instance.MaxInstance, id);
                }
                break;
            case EItemType.CHIP:
                AddChip(id, num);
                break;
            case EItemType.EXP:
                break;
            case EItemType.RUNE:
                for (int i = 0; i < num; i++)
                {
                    DataManager.Instance.MaxInstance++;
                    AddNewRune(DataManager.Instance.MaxInstance, id);
                }
                break;
            case EItemType.TASK:
            case EItemType.BOX:
            case EItemType.KEY:
            case EItemType.DRUG:
            case EItemType.MAT:
                AddItem(id, num);
                break;

        }
    }

    public void AddNewItemList(List<XStruct> list,bool showAward=true)
    {
        if(list==null)
        {
            return;
        }
        for(int i=0;i<list.Count;i++)
        {
            AddNewItem(list[i].Id, list[i].Num);
        }
        if(showAward)
        {
            //ZTDialog.ShowAwardWindow(list);
        }
    }

    public void AddExp(int exp)
    {
        if (exp <= 0)
        {
            return;
        }
        XRole role = DataManager.Instance.GetCurRole();
        int maxLevel = ZTConfig.Instance.DictHeroLevel.Count;
        if (role.Level >= maxLevel)
        {
            return;
        }
        role.CurExp += exp;
        DBHeroLevel levelDB = ZTConfig.Instance.GetDBHeroLevel(role.Level);
        while (role.CurExp >= levelDB.RequireExp)
        {
            role.CurExp -= levelDB.RequireExp;
            role.Level++;
            if (role.Level >= maxLevel)
            {
                role.CurExp = 0;
                break;
            }
            levelDB = ZTConfig.Instance.GetDBHeroLevel(role.Level);
        }
        DataManager.Instance.DataRole.Update(role.Id, role);
    }

    public void AddItem(int id, int num)
    {
        XItem item = DataManager.Instance.GetItemDataById(id);
        if (item == null)
        {
            int newPos = GetNewPos(EBagType.ITEM);
            if (newPos != 0)
            {
                DataManager.Instance.MaxInstance++;
                item = new XItem();
                item.Instance = DataManager.Instance.MaxInstance;
                item.Pos = newPos;
                item.Id = id;
                item.Num = num;
                item.PosType = (int)EPosType.BagItem;
                DataBagItem.Insert(newPos, item);
            }
        }
        else
        {
            item.Num += num;
            DataBagItem.Update(item.Pos, item);
        }
    }

    public void AddNewEquip(int instanceId, int id)
    {
        int newPos = GetNewPos(EBagType.ITEM);
        if (newPos == 0)
        {
            return;
        }
        XItem item = new XItem();
        item.Instance = instanceId;
        item.Pos = newPos;
        item.Id = id;
        item.Num = 1;
        item.PosType = (int)EPosType.BagItem;
        DataBagItem.Insert(newPos, item);

        XEquip equip = new XEquip();
        equip.Instance = instanceId;
        equip.Id = id;
        equip.StarLevel = 0;
        equip.StrengthenLevel = 0;
        equip.AdvanceLevel = 0;
        equip.StrengthenExp = 0;
        DataEquip.Insert(instanceId, equip);
    }

    public void AddNewGem(int instanceId, int id)
    {
        int newPos = GetNewPos(EBagType.GEM);
        if (newPos == 0)
        {
            return;
        }
        XItem item = new XItem();
        item.Instance = instanceId;
        item.Pos = newPos;
        item.Id = id;
        item.Num = 1;
        item.PosType = (int)EPosType.BagGem;
        DataBagGem.Insert(newPos, item);

        XGem gem = new XGem();
        gem.Instance = instanceId;
        gem.Id = id;
        gem.StrengthenLevel = 0;
        gem.StrengthenExp = 0;
        DataGem.Insert(instanceId, gem);
    }

    public void AddChip(int id, int num)
    {
        XItem item = DataManager.Instance.GetItemDataById(id);
        if (item == null)
        {
            int newPos = GetNewPos(EBagType.CHIP);
            if (newPos != 0)
            {
                DataManager.Instance.MaxInstance++;
                item = new XItem();
                item.Instance = DataManager.Instance.MaxInstance;
                item.Pos = newPos;
                item.Id = id;
                item.Num = num;
                item.PosType = (int)EPosType.BagChip;
                DataBagChip.Insert(newPos, item);
            }
        }
        else
        {
            item.Num += num;
            DataBagChip.Update(item.Pos, item);
        }
    }

    public void AddNewRune(int instanceId, int id)
    {
        int newPos = GetNewPos(EBagType.RUNE);
        if (newPos == 0)
        {
            return;
        }
        XItem item = new XItem();
        item.Pos = newPos;
        item.Instance = instanceId;
        item.Pos = newPos;
        item.Id = id;
        item.Num = 1;
        item.PosType = (int)EPosType.BagRune;
        DataBagRune.Insert(newPos, item);

        XRune rune = new XRune();
        rune.Instance = instanceId;
        rune.Id = id;
        rune.Level = 0;
        DataRune.Insert(instanceId, rune);
    }

    public void AddNewFashion(int instanceId, int id)
    {
        int newPos = GetNewPos(EBagType.FASHION);
        if (newPos == 0)
        {
            return;
        }
        XItem item = new XItem();
        item.Pos = newPos;
        item.Instance = instanceId;
        item.Id = id;
        item.Num = 1;
        item.PosType = (int)EPosType.BagFashion;
        DataBagFashion.Insert(newPos, item);
    }

    public void AddMoney(int id, int num)
    {

        XMoney money;
        if (DataManager.Instance.Moneys.ContainsKey(id))
        {
            money = DataManager.Instance.Moneys[id];
            money.Num += num;
            DataMoney.Update(id, money);
        }
        else
        {
            money = new XMoney();
            money.Id = id;
            money.Num = num;
            DataMoney.Insert(id, money);
        }
        ZTEvent.FireEvent(EventID.CHANGE_MONEY, id);
    }

    public void AddAction(int id, int num)
    {
        XAction action;
        if (DataManager.Instance.Actions.ContainsKey(id))
        {
            action = DataManager.Instance.Actions[id];
            action.Num += num;
            DataAction.Update(id, action);
        }
        else
        {
            action = new XAction();
            action.Id = id;
            action.Num = num;
            DataAction.Insert(id, action);
        }
        DBItem db = ZTConfig.Instance.GetDBItem(id);
        EAction type = ZTConfig.Instance.GetActionType(id);
        ZTEvent.FireEvent(EventID.CHANGE_ACTION,type);
    }

    public void AddSoul(int id,int num)
    {
        XSoul soul;
        Souls.TryGetValue(id, out soul);
        if(soul==null)
        {
            soul = new XSoul();
            soul.Id = id;
            soul.Num = num;
            DataSoul.Insert(id, soul);
        }
        else
        {
            soul.Num += num;
            DataSoul.Update(id, soul);
        }
    }

    public bool UseItemById(int id, int num = 1)
    {
        DBItem itemDB = ZTConfig.Instance.GetDBItem(id);
        switch (itemDB.ItemType)
        {
            case EItemType.MONEY:
                return UseMoney(id, num);
            case EItemType.ACTION:
                return UseAction(id, num);
            case EItemType.PETSOUL:
                return UseSoul(id, num);
            case EItemType.BOX:
            case EItemType.DRUG:
            case EItemType.KEY:
            case EItemType.MAT:
                return UseBagItem(id, num);
            case EItemType.EXP:
                return false;
            case EItemType.CHIP:
                return UseBagChip(id, num);
            default:
                return false;
        }


    }

    public bool UseMoney(int id, int num)
    {
        XMoney money = DataManager.Instance.Moneys[id];
        if (money == null)
        {
            return false;
        }
        if (money.Num < num)
        {
            return false;
        }
        money.Num -= num;
        DataMoney.Update(id, money);
        ZTEvent.FireEvent(EventID.CHANGE_MONEY,id);

        return true;
    }

    public bool UseAction(int id, int num)
    {
        XAction action = DataManager.Instance.Actions[id];
        if (action == null)
        {
            return false;
        }
        if (action.Num < num)
        {
            return false;
        }
        action.Num -= num;
        DataAction.Update(id, action);

        ZTEvent.FireEvent(EventID.CHANGE_ACTION,(EAction)(id-100));
        return true;
    }

    public bool UseSoul(int id, int num)
    {
        XSoul soul = null;
        Souls.TryGetValue(id, out soul);
        if(soul==null)
        {
            return false;
        }
        if(soul.Num<num)
        {
            return false;
        }
        soul.Num -= num;
        DataSoul.Update(id, soul);
        return true;
    }

    public bool UseBagItem(int id, int num)
    {
        XItem item = DataManager.Instance.GetItemDataById(id);
        if (item == null)
        {
            return false;
        }
        if (item.Num < num)
        {
            return false;
        }
        item.Num -= num;
        if (item.Num < 1)
        {
            int pos = item.Pos;
            DataBagItem.Delete(pos);
        }
        else
        {
            DataBagItem.Update(item.Pos,item);
        }
        return true;
    }

    public bool UseBagChip(int id, int num)
    {
        XItem item = DataManager.Instance.GetItemDataById(id);
        if (item == null)
        {
            return false;
        }
        if (item.Num < num)
        {
            return false;
        }
        item.Num -= num;
        if (item.Num < 1)
        {
            int pos = item.Pos;
            DataBagChip.Delete(pos);
        }
        else
        {
            DataBagChip.Update(item.Pos, item);
        }

        return true;
    }

    public bool DelBagEquip(int pos)
    {
        if (!DataManager.Instance.BagItems.ContainsKey(pos))
        {
            return false;
        }
        XItem item = DataManager.Instance.BagItems[pos];
        if (!DataManager.Instance.Equips.ContainsKey(item.Instance))
        {
            return false;
        }
        DataBagItem.Delete(pos);
        DataEquip.Delete(item.Instance);
        return true;
    }

    public bool DelBagGem(int pos)
    {
        if (!DataManager.Instance.BagGems.ContainsKey(pos))
        {
            return false;
        }
        XItem xp = DataManager.Instance.BagGems[pos];
        if (!DataManager.Instance.Gems.ContainsKey(xp.Instance))
        {
            return false;
        }
        DataBagGem.Delete(pos);
        DataGem.Delete(xp.Instance);
        return true;
    }

    public bool DelBagFashion(int pos)
    {
        if (!DataManager.Instance.BagFashions.ContainsKey(pos))
        {
            return false;
        }
        XItem item = DataManager.Instance.BagFashions[pos];
        DataBagFashion.Delete(item.Pos);
        return true;
    }

    public bool DelBagRune(int pos)
    {
        if (!DataManager.Instance.BagRunes.ContainsKey(pos))
        {
            return false;
        }
        XItem xp = DataManager.Instance.BagRunes[pos];
        if (!DataManager.Instance.Runes.ContainsKey(xp.Instance))
        {
            return false;
        }
        DataBagRune.Delete(xp.Pos);
        DataRune.Delete(xp.Instance);
        return true;
    }

    public bool AddRole(int roleId, string name)
    {
        if (Roles.ContainsKey(roleId))
        {
            return false;
        }
        XRole role = new XRole();
        role.Id = roleId;
        role.Level = 1;
        role.Name = name;
        role.VipLevel = 0;
        role.CurExp = 0;
        role.Position = Vector3.zero;
        role.Rotation = Vector3.zero;
        DataRole.Insert(roleId, role);
        return true;
    }

    public bool SetMount(int mountID)
    {
        XRole role = DataManager.Instance.GetCurRole();
        if(role==null)
        {
            return false;
        }
        role.MountID = mountID;
        DataRole.Update(role.Id, role);
        return true;
    }

    public XEquip GetDressEquipByPos(int pos)
    {
        if(!DressEquips.ContainsKey(pos))
        {
            return null;
        }
        XItem item = DressEquips[pos];
        XEquip equip = null;
        Equips.TryGetValue(item.Instance, out equip);
        return equip;
    }

    public XRelics GetRelicsById(int id)
    {
        XRelics relics = null;
        Relics.TryGetValue(id, out relics);
        return relics;
    }

    public XPet GetPetById(int id)
    {
        XPet data = null;
        Pets.TryGetValue(id, out data);
        return data;
    }

    public XPartner GetPartnerById(int id)
    {
        XPartner data = null;
        Partners.TryGetValue(id, out data);
        return data;
    }

    public XThreadTask GetThreadTaskDataById(int id)
    {
        XThreadTask data = null;
        ThreadTasks.TryGetValue(id, out data);
        return data;
    }

    public XBranchTask GetBranchTaskDataById(int id)
    {
        XBranchTask data = null;
        BranchTasks.TryGetValue(id, out data);
        return data;
    }

    public XDailyTask GetDailyTaskDataById(int id)
    {
        XDailyTask data = null;
        DailyTasks.TryGetValue(id, out data);
        return data;
    }

    private void ActionStart()
    {
        mActionKeyList.Clear();
        mNextActionTimes.Clear();
        Dictionary<int, DBItem>.Enumerator em = ZTConfig.Instance.DictAction.GetEnumerator();
        while (em.MoveNext())
        {
            DBItem db = em.Current.Value;
            int id = db.Id;
            int count = DataManager.Instance.GetItemCountById(id);
            long timer = CurServerTime;
            if (count < db.Data1)
            {
                timer += db.Data2;
            }
            mActionKeyList.Add(id);
            mNextActionTimes.Add(timer);
        }
        em.Dispose();
        ZTEvent.AddHandler(EventID.SECOND_TICK, ActionUpdate);
    }

    private void ActionExit()
    {
        ZTEvent.RemoveHandler(EventID.SECOND_TICK, ActionUpdate);
    }

    private void ActionUpdate()
    {
        for (int i = 0; i < mActionKeyList.Count; i++)
        {
            int key = mActionKeyList[i];
            DBItem db = ZTConfig.Instance.GetDBItem(key);
            EAction actionType = ZTConfig.Instance.GetActionType(key);
            int count = GetActionCountByType(actionType);
            if (count >= db.Data1)
            {
                mNextActionTimes[i] = CurServerTime;
            }
            else
            {
                if (CurServerTime >= mNextActionTimes[i])
                {
                    AddAction(key, 1);
                    mNextActionTimes[i] = CurServerTime + db.Data2;
                }
            }
        }
        ZTEvent.FireEvent(EventID.UPDATE_ACTION_TIMER);
    }

    public bool IsActionFull(EAction actionType)
    {
        DBItem db = ZTConfig.Instance.GetDBAction(actionType);
        int count = GetActionCountByType(actionType);
        return count >= db.Data1;
    }

    public bool IsActionFull(int key)
    {
        DBItem db = ZTConfig.Instance.GetDBItem(key);
        int count = GetItemCountById(key);
        return count >= db.Data1;
    }

    public int GetOneRemainSeconds(EAction actionType)
    {
        int index = 0;
        for (int i = 0; i < mActionKeyList.Count; i++)
        {
            int key = mActionKeyList[i];
            DBItem db = ZTConfig.Instance.GetDBItem(key);
            EAction type = ZTConfig.Instance.GetActionType(key);
            if (type == actionType)
            {
                index = i;
                break;
            }
        }
        return (int)(mNextActionTimes[index] - CurServerTime);
    }

    public int GetAllRemainSeconds(EAction actionType)
    {
        int index = 0;
        for (int i = 0; i < mActionKeyList.Count; i++)
        {
            int key = mActionKeyList[i];
            EAction type = ZTConfig.Instance.GetActionType(key);
            if (type == actionType)
            {
                index = i;
                break;
            }
        }
        int count = GetActionCountByType(actionType);
        DBItem db = ZTConfig.Instance.GetDBAction(actionType);
        return (int)((db.Data1 - count - 1) * db.Data2 + mNextActionTimes[index] - CurServerTime);
    }

    public string GetOneSecondTimer(EAction actionType)
    {
        int sec = this.GetOneRemainSeconds(actionType);
        return GTTools.SecondsToTimer(sec).ToString();
    }

    public string GetAllSecondTimer(EAction actionType)
    {
        int sec = this.GetAllRemainSeconds(actionType);
        return GTTools.SecondsToTimer(sec).ToString();
    }
}