using UnityEngine;
using System.Collections;

namespace BT
{
    public class Effect : BTTask
    {
        public string JudgeName = string.Empty;
        public EffectData Data = new EffectData();

        private Transform mCacheTransform = null;
        private EffectBase mEffectEntiny = null;

        protected override void ReadAttribute(string key, string value)
        {
            //if (this.Data.Id == 61009)
            {
                Debug.LogError(key + "=======" + value);
            }
            switch (key)
            {
                case "Id":
                    this.Data.Id = value.ToInt32();
                    break;
                case "LastTime":
                    this.Data.LastTime = value.ToFloat();
                    break;
                case "Bind":
                    this.Data.Bind = (EEffectBind)value.ToInt32();
                    break;
                case "Fly":
                    this.Data.Fly = (EFlyType)value.ToInt32();
                    break;
                case "SetParent":
                    this.Data.SetParent = value.ToInt32() == 1;
                    break;
                case "Dead":
                    this.Data.Dead = (EFlyObjDeadType)value.ToInt32();
                    break;
                case "Offset":
                    this.Data.Offset = GTTools.ToVector3(value, true);
                    break;
                case "Euler":
                    this.Data.Euler = GTTools.ToVector3(value, true);
                    break;
                case "Sound":
                    this.Data.Sound = value;
                    break;
                case "FlySpeed":
                    this.Data.FlySpeed = value.ToFloat();
                    break;
                case "Delay":
                    this.Data.Delay = value.ToFloat();
                    break;
                case "JudgeName":
                    this.JudgeName = value;
                    break;

            }
        }

        protected override bool Enter()
        {
            base.Enter();
            if (Owner == null)
            {
                return false;
            }
            Data.Owner = Owner;
            Data.Target = Owner.GetTarget();
            Data.Parent = Owner.CacheTransform;
            this.mEffectEntiny = ZTEffect.Instance.CreateEffect(Data);
            ZTAudio.Instance.PlayEffectAudio(GTTools.Format("Sound/Sound/{0}", Data.Sound));
            return true;
        }

        protected override EBTStatus Execute()
        {
            if (mEffectEntiny == null)
            {
                return EBTStatus.BT_FAILURE;
            }

            EEffectState pState = mEffectEntiny.State;
            switch (pState)
            {
                case EEffectState.Error:
                    {
                        return EBTStatus.BT_FAILURE;
                    }
                case EEffectState.Update:
                    {
                        if (mCacheTransform == null && mEffectEntiny.CacheTransform != null)
                        {
                            mCacheTransform = mEffectEntiny.CacheTransform;
                            BTTreeManager.Instance.SaveData(this, JudgeName, mCacheTransform);
                        }

                        object var = BTTreeManager.Instance.GetData(this, Define.BT_JUDGE_SUCCESS);
                        if (var != null && Data.Dead == EFlyObjDeadType.UntilColliderTar)
                        {
                            mEffectEntiny.SwitchState(EEffectState.Dead);
                        }
                        return EBTStatus.BT_RUNNING;
                    }
                case EEffectState.Wait:
                    {
                        return EBTStatus.BT_RUNNING;
                    }
                case EEffectState.Dead:
                    {
                        mEffectEntiny = null;
                        return EBTStatus.BT_SUCCESS;
                    }
                default:
                    {
                        return EBTStatus.BT_FAILURE;
                    }
            }
            return EBTStatus.BT_FAILURE;
        }

        public override void Clear()
        {
            base.Clear();
            mEffectEntiny = null;
            mCacheTransform = null;
        }

        public override BTNode DeepClone()
        {
            Effect effect = new Effect();
            effect.Data = this.Data;
            effect.JudgeName = this.JudgeName;
            effect.Owner = BTTreeManager.Instance.GetOwnerByNode(this);
            effect.CloneChildren(this);
            return effect;
        }
    }
}

