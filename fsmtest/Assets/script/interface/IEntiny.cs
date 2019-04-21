using UnityEngine;
using System.Collections;

//public class XTransform
//{
//    public Vector3 Position = Vector3.zero;
//    public Vector3 EulerAngles = Vector3.zero;
//    public Vector3 Scale = Vector3.one;

//    public static XTransform Create(Vector3 pos, Vector3 angle)
//    {
//        XTransform data = new XTransform();
//        data.Position = pos;
//        data.EulerAngles = angle;
//        data.Scale = Vector3.one;
//        return data;
//    }

//    public static XTransform Create(Vector3 pos, Vector3 angle, Vector3 scale)
//    {
//        XTransform data = new XTransform();
//        data.Position = pos;
//        data.EulerAngles = angle;
//        data.Scale = scale;
//        return data;
//    }

//    static XTransform mDefault = new XTransform();
//    public static XTransform Default { get { return mDefault; } }
//}

public interface IEntiny : IObj
{
    int  GUID { get; set; }
    void Load(XTransform param);
    void Destroy();
    void Step();
    void Pause(bool pause);
    bool IsDead();
    bool IsDestroy();
}
