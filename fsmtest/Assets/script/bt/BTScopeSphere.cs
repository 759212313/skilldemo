using UnityEngine;
using System.Collections;

namespace BT
{
    public class ScopeSphere : BTScopeShape
    {
        public float Radius;
        public Collider collider;

        public override bool IsTouch(Actor actor)
        {
            if (actor == null || actor.CacheTransform == null)
            {
                return false;
            }
            if (Radius <= 0)
            {
                return false;
            }
            if (GTTools.GetHorizontalDistance(Center.position, actor.Pos) < actor.Radius)
            {
                return true;
            }
            return false;
        }
    }
}