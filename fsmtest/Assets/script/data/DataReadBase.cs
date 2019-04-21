using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

public interface IDataReadBase<T>
{
    void Insert(int key,T obj);
    void Delete(int key);
    void Update(int key,T obj);
    void ClearAll();
}

public abstract class DataReadBase<T>:IDataReadBase<T> where T :XModule,new ()
{
    public string xmlPath;
    public DataKeyType keyType;
    public Dictionary<int, T> dict = null;

    public void AppendAttribute(int key, string name, string value)
    {
        if (dict == null)
        {
            return;
        }
        T obj = default(T);
        dict.TryGetValue(key, out obj);
        if (obj == null)
        {
            obj = new T();
            dict.Add(key, obj);
        }
        FieldInfo[] fields = obj.GetType().GetFields();
        for (int i = 0; i < fields.Length; i++)
        {
            FieldInfo field = fields[i];
            if (field.Name == name)
            {
                Parse(ref field, obj, value);
                break;
            }
        }
    }

    void Parse(ref FieldInfo field,object obj,string value)
    {
        object fieldType = field.GetValue(obj);
        if (fieldType is int)
        {
            field.SetValue(obj, value.ToInt32());
        }
        else if (fieldType is string)
        {
            field.SetValue(obj, value);
        }
        else if (fieldType is Vector3)
        {
            field.SetValue(obj, value.ToVector3(true));
        }
    }

    public virtual void Insert(int key, T obj)
    { 
        EXml.Append(xmlPath, key, obj, keyType);
        if(dict!=null)
        {
            dict[key] = obj;
        }
    }

    public virtual void Delete(int key)
    {
        EXml.Delete(xmlPath, key,keyType);
        if (dict != null)
        {
            dict.Remove(key);
        }
    }

    public virtual void Update(int key,T obj)
    {
        if(dict.ContainsKey(key))
        {
            EXml.Update(xmlPath, key, obj, keyType);
        }
        else
        {
            Insert(key, obj);
        }

    }

    public T GetDataById(int key)
    {
        if(dict==null)
        {
            return null;
        }
        T obj = null;
        dict.TryGetValue(key, out obj);
        return obj;
    }

    public void ClearAll()
    {
        EXml.ClearAll(xmlPath);
        if (dict != null)
        {
            dict.Clear();
        }
    }
}

public sealed class DataReadBagFashion : DataReadBase<XItem> { }
public sealed class DataReadBagGem : DataReadBase<XItem> { }
public sealed class DataReadBagItem : DataReadBase<XItem> { }
public sealed class DataReadBagRune : DataReadBase<XItem> { }
public sealed class DataReadDressFashion : DataReadBase<XItem> { }
public sealed class DataReadDressGem : DataReadBase<XItem> { }
public sealed class DataReadDressRune : DataReadBase<XItem> { }
public sealed class DataReadEquip : DataReadBase<XEquip> { }
public sealed class DataReadGem : DataReadBase<XGem> { }
public sealed class DataReadMoney : DataReadBase<XMoney> { }
public sealed class DataReadSoul : DataReadBase<XSoul> { }
public sealed class DataReadRaid : DataReadBase<XRaid> { }
public sealed class DataReadMainChapter : DataReadBase<XMainChapter> { }
public sealed class DataReadCopy : DataReadBase<XCopy> { }
public sealed class DataReadRole : DataReadBase<XRole> { }
public sealed class DataReadRune : DataReadBase<XRune> { }
public sealed class DataReadAction : DataReadBase<XAction> { }
public sealed class DataReadBagChip : DataReadBase<XItem> { }
public sealed class DataReadDressEquip : DataReadBase<XItem> { }
public sealed class DataReadMount : DataReadBase<XMount> { }
public sealed class DataReadRelics : DataReadBase<XRelics> { }
public sealed class DataReadPartner : DataReadBase<XPartner> { }
public sealed class DataReadPet : DataReadBase<XPet> { }
public sealed class DataReadGuide : DataReadBase<XGuide> { }
public sealed class DataReadThreadTask : DataReadBase<XThreadTask> { }
public sealed class DataReadBranchTask : DataReadBase<XBranchTask> { }
public sealed class DataReadDailyTask : DataReadBase<XDailyTask> { }
