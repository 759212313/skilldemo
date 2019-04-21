using UnityEngine;
using System.Collections;

namespace BT
{

    public class ScopeBox : BTScopeShape
    {
        public float L;     //长
        public float W;     //宽
        public float H;     //高

        public override bool IsTouch(Actor actor)
        {
            if (actor == null || actor.CacheTransform == null)
            {
                return false;
            }
            if (H <= 0 || W <= 0 || H <= 0)
            {
                return false;
            }

            float y = Center.position.y;
            float yMax = y + H;
            float yMin = y;

            if (actor.Pos.y + actor.Height <= yMin)
            {
                return false;
            }
            if (actor.Pos.y >= yMax)
            {
                return false;
            }

            Vector3 targetPos = actor.Pos;
            targetPos.y = 0;
            Vector3 centerPos = Center.position;
            centerPos.y = 0;

            Vector3 forward = Center.forward + Euler;
            float angle = Vector3.Angle(targetPos - centerPos, forward);
            if (angle > 90)
            {
                return false;
            }
            float w = W / 2;
            float checkAngle = Mathf.Atan2(w, L) * Mathf.Rad2Deg;
            float distance = Vector3.Distance(targetPos, centerPos);
            if (angle <= checkAngle)
            {
                if (distance > L / Mathf.Cos(angle / Mathf.Rad2Deg))
                {
                    return false;
                }
            }
            else
            {
                if (distance > w / Mathf.Sin(angle / Mathf.Rad2Deg))
                {
                    return false;
                }
            }
            return true;
        }
    }
}