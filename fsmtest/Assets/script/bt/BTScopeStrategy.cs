using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BT
{
    public interface IScopeStrategy
    {
        bool IsTouch(Actor actor);
    }

    public class BTScopeShape : BTNode,IScopeStrategy
    {
        public Transform Center;
        public Vector3 Offset;   //离施法者中心的位置
        public Vector3 Euler;    //离施法者中心的旋转角度

        public virtual bool IsTouch(Actor actor)
        {
            return false;
        }
    }
}

