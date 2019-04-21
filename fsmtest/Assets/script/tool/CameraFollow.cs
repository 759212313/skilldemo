using UnityEngine;
using System.Collections;

public class CameraFollow :CameraEffectBase
{
    public CameraFollow()
    {
        mType = ECameraType.FOLLOW;
    }

    public float distance=-12;
    public float height = 6;
    public float angle = 90;
    public Transform Follow;

    public override void OnUpdate()
    {
        if (Follow == null)
        {
            return;
        }
        Vector3 pos = Follow.position + new Vector3(0, height, distance);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5);
        transform.LookAt(Follow);
    }

    public override void Init(int id, Camera cam, CameraEvent callback, params object[] args)
    {
        base.Init(id, cam, callback, args);
        Follow = (Transform)args[0];
        Vector3 pos = Follow.position + Vector3.up * height + Follow.forward * distance;
        transform.position = pos;
        transform.LookAt(Follow); 
    }
}
