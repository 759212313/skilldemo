using UnityEngine;
using System.Collections;

namespace BT
{
    public class Sequence : Composite
    {
        public override EBTStatus Step()
        {
            if (mActiveIndex == -1)
            {
                mActiveIndex = 0;
            }
            for (; mActiveIndex < mChildren.Count; mActiveIndex++)
            {
                BTNode pNode = mChildren[mActiveIndex];
                EBTStatus pStatus = pNode.Step();
                switch (pStatus)
                {
                    case EBTStatus.BT_RUNNING:
                        {
                            mIsRunning = true;
                            return EBTStatus.BT_RUNNING;
                        }
                    case EBTStatus.BT_SUCCESS:
                        {
                            pNode.Clear();
                            continue;
                        }
                    case EBTStatus.BT_FAILURE:
                        {
                            mActiveIndex = -1;
                            mIsRunning = false;
                            pNode.Clear();
                            return EBTStatus.BT_FAILURE;
                        }
                }
            }
            mActiveIndex = -1;
            mIsRunning = false;
            return EBTStatus.BT_SUCCESS;
        }


        public override BTNode DeepClone()
        {
            Sequence sequence = new Sequence();
            sequence.CloneChildren(this);
            return sequence;
        }
    }
}