using UnityEngine;
using System.Collections;

namespace BT
{
    public class ScopeCylinder : BTScopeShape
    {
        public float MaxDis = 2;    //最大有效距离
        public float HAngle = 360;  //水平角度范围
        public float Height = 2;    //圆柱有效高度

        public override bool IsTouch(Actor actor)
        {
            if (actor == null || actor.CacheTransform == null)
            {
                return false;
            }
            if (HAngle <= 0 || Height <= 0)
            {
                return false;
            }


            float y = Center.position.y;
            float yMax = y + Height / 2;
            float yMin = y - Height / 2;
            if (actor.Pos.y + actor.Height <= yMin)
            {
                return false;
            }
            if (actor.Pos.y >= yMax)
            {
                return false;
            }

            Vector3 dirPos = Euler + Center.forward;
            dirPos.y = 0;
            float radius = MaxDis + actor.Radius;
            if (GTTools.GetHorizontalDistance(Center.position, actor.Pos) > radius)
            {
                return false;
            }

            Vector3 targetPos = actor.Pos;
            targetPos.y = 0;
            Vector3 centerPos = Center.position;
            centerPos.y = 0;
            if (Vector3.Angle(targetPos - centerPos, dirPos) > HAngle / 2)
            {
                return false;
            }
            return true;
        }
    }
}