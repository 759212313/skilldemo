using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System;
using System.Collections.Generic;

public abstract class CfgObj
{
    static char[] SEPARATOR = new char[3] { '(', ',', ')' };

    public abstract void Read(XmlElement os);
    public abstract void Write(XmlDocument doc, XmlElement os);

    public static Int32    ReadInt32(string value)
    {
        int v = 0;
        int.TryParse(value, out v);
        return v;
    }

    public static float    ReadFloat(string value)
    {
        float  v = 0;
        float.TryParse(value, out v);
        return v;
    }

    public static Vector3  ReadVector3(string value)
    {
        string[] array = value.Split(SEPARATOR);
        return new Vector3(array[1].ToFloat(), array[2].ToFloat(), array[3].ToFloat());
    }

    public static Boolean  ReadBool(string value)
    {
        return value == "1" ? true : false;
    }

    public static T        ReadObj<T>(XmlElement os) where T : CfgObj
    {
        Type type = typeof(T);
        try
        {
            object v = Activator.CreateInstance(type);
            T result = (T)v;
            result.Read(os);
            return result;
        }
        catch
        {
            Debug.LogError("XmlElement is null:" + type.ToString());
            return null;
        }
    }

    public static void Write(XmlDocument doc,XmlElement os, string name, int v)
    {
        os.SetAttribute(name, v.ToString());
    }

    public static void Write(XmlDocument doc,XmlElement os, string name, float v)
    {
        os.SetAttribute(name, v.ToString("0.00"));
    }

    public static void Write(XmlDocument doc,XmlElement os, string name, bool v)
    {
        os.SetAttribute(name, (v ? 1 : 0).ToString());
    }

    public static void Write(XmlDocument doc,XmlElement os, string name, Vector3 v)
    {
        string s = string.Format("({0},{1},{2})", v.x.ToString("0.00"), v.y.ToString("0.00"), v.z.ToString("0.00"));
        os.SetAttribute(name, s);
    }

    public static void Write(XmlDocument doc, XmlElement os, string name, string v)
    {
        os.SetAttribute(name, v);
    }

    public static void Write<T>(XmlDocument doc, XmlElement os, string name, T obj) where T : CfgObj
    {
        XmlElement child = doc.CreateElement(name);
        obj.Write(doc, child);
        os.AppendChild(child);
    }
}
