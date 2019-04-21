using UnityEngine;
using System.Collections;

public class XStruct
{
    public int Id;
    public int Num;

    public XStruct(int id, int num)
    {
        this.Id = id;
        this.Num = num;
    }

    public XStruct()
    {

    }
}

public class XTransform
{
    public Vector3 Position = Vector3.zero;
    public Vector3 EulerAngles = Vector3.zero;
    public Vector3 Scale = Vector3.one;

    public static XTransform Create(Vector3 pos, Vector3 angle)
    {
        XTransform data = new XTransform();
        data.Position = pos;
        data.EulerAngles = angle;
        data.Scale = Vector3.one;
        return data;
    }

    public static XTransform Create(Vector3 pos, Vector3 angle, Vector3 scale)
    {
        XTransform data = new XTransform();
        data.Position = pos;
        data.EulerAngles = angle;
        data.Scale = scale;
        return data;
    }

    static XTransform mDefault = new XTransform();
    public static XTransform Default { get { return mDefault; } }
}


public enum EPosType
{

    BagItem,
    BagGem,
    BagFashion,
    BagRune,
    BagChip,
    RoleEquip,
    RoleGem,
    RoleRune,
    RoleFashion,
    RoleMoney,
    RoleAction,
    Empty,
}

public class XModule
{

    public XModule()
    {

    }
}


public class XMainChapter : XModule
{
    public int Id;
    public int Award1;
    public int Award2;
    public int Award3;

    public void OnReceiveAward(int awardIndex)
    {
        switch (awardIndex)
        {
            case 0:
                Award1 = 1;
                break;
            case 1:
                Award2 = 1;
                break;
            case 3:
                Award3 = 1;
                break;
        }
    }
}

public class XCopy : XModule
{
    public int Id;
    public int StarNum;
}

public class XRaid : XModule
{
    public int Id;
    public int MaxChapter;
    public int MaxCopyId;

}

public class XRole : XModule
{
    public int Id;
    public string Name = string.Empty;
    public int Level;
    public int CurExp;
    public int VipLevel;
    public int VipExp;
    public int SceneID;
    public Vector3 Position;
    public Vector3 Rotation;
    public int MountID;
    public int RelicID;
    public int PetID;
    public int PartnerID1;
    public int PartnerID2;
    public int PartnerID3;
    public int TalentMask;

    public int GetPartnerIDByPos(int pos)
    {
        switch (pos)
        {
            case 1:
                return PartnerID1;
            case 2:
                return PartnerID2;
            case 3:
                return PartnerID3;
            default:
                return 0;
        }
    }

    public void SetPartnerIDByPos(int pos, int id)
    {
        switch (pos)
        {
            case 1:
                PartnerID1 = id;
                break;
            case 2:
                PartnerID2 = id;
                break;
            case 3:
                PartnerID3 = id;
                break;

        }
    }

    public void ActiveTalentByLayerAndPos(int layer, int pos)
    {
        int a = (int)Mathf.Pow(10, layer - 1);
        int m = TalentMask % a;
        int b = TalentMask / a;
        int c = (b % 10) * 10 + pos;
        TalentMask = c * a + m;
    }

    public void ResetAllLayers()
    {
        TalentMask = 0;
    }

    public int GetTalentByLayer(int layer)
    {
        int a = (int)Mathf.Pow(10, layer - 1);
        int b = TalentMask / a;
        return b - (b % 10) * 10;
    }

    public int MaxExp
    {
        get
        {
            return ZTConfig.Instance.GetDBHeroLevel(Level).RequireExp;
        }
    }

    public ECarrer Carrer
    {
        get { return (ECarrer)Id; }
    }
}



public class XItem : XModule
{
    public int Pos;
    public int Instance;
    public int PosType;
    public int Id;
    public int Num;

    public XItem Clone()
    {
        XItem clone = new XItem();
        clone.Pos = this.Pos;
        clone.Instance = this.Instance;
        clone.PosType = this.PosType;
        clone.Id = this.Id;
        clone.Num = this.Num;
        return clone;
    }
}

public class XMoney : XModule
{
    public int Id;
    public int Num;
}


public class XAction : XModule
{
    public int Id;
    public int Num;
}

public class XSoul : XModule
{
    public int Id;
    public int Num;
}

public class XRune : XModule
{
    public int Instance;
    public int Id;
    public int Level;
}

public class XEquip : XModule
{
    public int Instance;
    public int Id;
    public int StrengthenLevel;
    public int StrengthenExp;
    public int StarLevel;
    public int AdvanceLevel;

    public XEquip Clone()
    {
        XEquip e = new XEquip();
        e.Id = this.Id;
        e.Instance = this.Instance;
        e.StarLevel = this.StarLevel;
        e.AdvanceLevel = this.AdvanceLevel;
        e.StrengthenLevel = this.StrengthenLevel;
        e.StrengthenExp = this.StrengthenExp;
        return e;
    }
}

public class XGem : XModule
{
    public int Instance;
    public int Id;
    public int StrengthenLevel;
    public int StrengthenExp;

    public XGem Clone()
    {
        XGem gem = new XGem();
        gem.Instance = Instance;
        gem.Id = Id;
        gem.StrengthenLevel = StrengthenLevel;
        gem.StrengthenExp = StrengthenExp;
        return gem;
    }
}

public class XPartner : XModule
{
    public int Id;
    public int Level;
    public int Advance;
    public int Star;
    public int Exp;
    public int Wake;

    public static XPartner New(int id)
    {
        XPartner tab = new XPartner();
        tab.Id = id;
        tab.Level = 0;
        tab.Advance = 0;
        return tab;
    }
}

public class XMount : XModule
{
    public int Id;
    public int Level;
}

public class XRelics : XModule
{
    public int Id;
    public int Level;
    public int CurExp1;
    public int CurExp2;
    public int CurExp3;

    public int GetExp(int index)
    {
        switch (index)
        {
            case 1:
                return CurExp1;
            case 2:
                return CurExp2;
            case 3:
                return CurExp3;
            default:
                return 0;
        }
    }

    public void AddExp(int index, int num)
    {
        switch (index)
        {
            case 1:
                CurExp1 += num;
                break;
            case 2:
                CurExp2 += num;
                break;
            case 3:
                CurExp3 += num;
                break;
        }
    }
}

public class XPet : XModule
{
    public int Id;
    public int Level;
    public int CurExp;

    public static XPet New(int id)
    {
        XPet data = new XPet();
        data.Level = 1;
        data.Id = id;
        data.CurExp = 0;
        return data;
    }
}

public class XPlayer : XModule
{
    public XRole Role;
    public XEquip[] Equips;
    public XGem[] Gems;
}

public class XSkill : XModule
{
    public int Id;
    public int Level;
}

public class XGuide : XModule
{
    public int Id;
    public int GuideIndex;
}

public class XDailyTask : XModule
{
    public int Id;
    public int Count;
    public int State;
}

public class XThreadTask : XModule
{
    public int CurTaskID;
    public int CurTaskStep;
}

public class XBranchTask : XModule
{
    public int Id;
    public int CurTaskStep;
}
