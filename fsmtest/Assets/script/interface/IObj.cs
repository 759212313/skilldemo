using UnityEngine;
using System.Collections;

public interface IObj
{
    int        Id             { get; set; }
    GameObject Obj            { get; set; }
    Transform  CacheTransform { get; set; }
}
