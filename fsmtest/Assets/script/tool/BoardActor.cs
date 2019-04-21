using UnityEngine;
using System.Collections;

public class BoardActor : BoardBase
{
    public override void Init()
    {
        base.Init();
        Actor actor = Owner as Actor;
        if (actor == null || actor.CacheTransform == null)
        {
            return;
        }
        SetTarget(actor.CacheTransform);
        mHeight = actor.Height + 0.5f;
    }
}