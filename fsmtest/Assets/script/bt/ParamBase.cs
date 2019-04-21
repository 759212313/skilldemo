using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

namespace Cfg.Act
{
    public abstract class ParamBase
    {
        public string Get(int index, string[] array)
        {
            return (index > array.Length - 1) ? string.Empty : array[index];
        }

        public string Set(int index, string value, ref string s)
        {
            s = index == 0 ? value : s + "," + value;
            return s;
        }

        public string Set(int index, int value, ref string s)
        {
            s = index == 0 ? value.ToString() : s + "," + value;
            return s;
        }

        public string Set(int index, float value, ref string s)
        {
            s = index == 0 ? value.ToString("0.00") : s + "," + value;
            return s;
        }

        public string Set(int index, Vector3 value, ref string s)
        {
            string str = string.Format("({0},{1},{2})", value.x.ToString("0.00"), value.y.ToString("0.00"), value.z.ToString("0.00"));
            s = index == 0 ? str : s + "," + value;
            return s;
        }

        public abstract void Save(ref string s);

        public abstract void Read(string[] array);

        public void Parse(string s)
        {
            string[] array = s.Split(',');
            Read(array);
        }
    }
}