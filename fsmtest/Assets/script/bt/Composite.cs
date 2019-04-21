using UnityEngine;
using System.Collections;

namespace BT
{
    public class Composite : BTNode
    {
        protected int mActiveIndex = -1;
        protected int mPrevioIndex = -1;

        public override void Clear()
        {
            base.Clear();
            mActiveIndex = -1;
            mPrevioIndex = -1;
        }
    }
}
