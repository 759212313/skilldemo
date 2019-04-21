using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace BT
{
    public class Action : BTNode
    {
        private EBTStatus mStatus = EBTStatus.BT_INITIAL;
        private EBTStatus mResult = EBTStatus.BT_INITIAL;

        public sealed override EBTStatus Step()
        {
            mResult = EBTStatus.BT_SUCCESS;
            if (mStatus == EBTStatus.BT_INITIAL)
            {
                bool checkCondition = Enter();
                mStatus = checkCondition ? EBTStatus.BT_RUNNING : EBTStatus.BT_FAILURE;
            }
            if (mStatus == EBTStatus.BT_RUNNING)
            {
                bool success = Prepare();
                if (success)
                {
                    mResult = Execute();
                    if (mResult != EBTStatus.BT_RUNNING)
                    {
                        Exit();
                        mStatus = EBTStatus.BT_INITIAL;
                        mIsRunning = false;
                    }
                    else
                    {
                        mIsRunning = true;
                    }
                }
                else
                {
                    mIsRunning = true;
                    return EBTStatus.BT_RUNNING;
                }
            }
            return mResult;
        }

        protected virtual EBTStatus Execute()
        {
            return EBTStatus.BT_FAILURE;
        }

        protected virtual bool Enter()
        {
            return true;
        }

        protected virtual bool Prepare()
        {
            return true;
        }

        protected virtual void Exit()
        {

        }

        public override void Clear()
        {
            base.Clear();
            if(mStatus!=EBTStatus.BT_INITIAL)
            {
                Exit();
                mStatus = EBTStatus.BT_INITIAL;
            }
        }
    }
}

