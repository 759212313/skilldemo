using UnityEngine;
using System.Collections;
using System;
using System.Xml;

namespace BT
{
    public class Anim : BTTask
    {
        public string AnimName = string.Empty;
        public string JudgeName = string.Empty;
        public string Sound = string.Empty;
        public Single Delay;
        public bool IsLoop;

        private float mUpdateTimer;
        private float mLastTime;
        private bool  mChildFinished = false;

        protected override void ReadAttribute(string key, string value)
        {
            switch (key)
            {
                case "AnimName":
                    this.AnimName = value;
                    break;
                case "IsLoop":
                    this.IsLoop = value.ToInt32() == 0 ? false : true;
                    break;
                case "Delay":
                    this.Delay = value.ToFloat();
                    break;
                case "JudgeName":
                    this.JudgeName = value;
                    break;
                case "Sound":
                    this.Sound = value;
                    break;
            }
        }

        protected override bool Enter()
        {
            base.Enter();
            Owner.GetActorAction().Play(AnimName, null, IsLoop);
            mUpdateTimer = Time.realtimeSinceStartup;
            mLastTime = Owner.GetActorAction().GetAnimLength(AnimName);

            BTTreeManager.Instance.SaveData(this, JudgeName, Owner.CacheTransform);
            //ZTAudio.Instance.PlayEffectAudio(GTTools.Format("Sound/Sound/{0}",Sound));
            return true;
        }

        protected override EBTStatus Execute()
        {
            if (Time.realtimeSinceStartup - mUpdateTimer > mLastTime)
            {
                return EBTStatus.BT_SUCCESS;
            }
            else
            {
                return EBTStatus.BT_RUNNING;
            }
        }

        public override void Clear()
        {
            base.Clear();
            mUpdateTimer = 0;
            mChildFinished = false;
        }

        public override BTNode DeepClone()
        {
            Anim anim = new Anim();
            anim.IsLoop = this.IsLoop;
            anim.Delay = this.Delay;
            anim.AnimName = this.AnimName;
            anim.JudgeName = this.JudgeName;
            anim.Sound = this.Sound;
            return anim;
        }
    }
}