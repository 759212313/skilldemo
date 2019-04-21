using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BT
{

    public class Parallel : Composite
    {
        private List<int> mFinishChildrenIndexList = new List<int>();

        public override EBTStatus Step()
        {
            for (int i = 0; i < mChildren.Count; i++)
            {
                if (mFinishChildrenIndexList.Contains(i))
                {
                    continue;
                }
                BTNode pNode = mChildren[i];
                EBTStatus pStatus = pNode.Step();

                if (pStatus != EBTStatus.BT_RUNNING)
                {
                    pNode.Clear();
                    mFinishChildrenIndexList.Add(i);
                }
            }

            mIsRunning = (mFinishChildrenIndexList.Count < mChildren.Count);
            return (mIsRunning) ? EBTStatus.BT_RUNNING : EBTStatus.BT_SUCCESS;
        }

        public override void Clear()
        {
            base.Clear();
            mFinishChildrenIndexList.Clear();
        }

        public override BTNode DeepClone()
        {
            Parallel parallel = new Parallel();
            parallel.CloneChildren(this);
            return parallel;
        }
    }
}
