using UnityEngine;
using System.Collections;

namespace BT
{
    public class ShakeScreen : Action
    {
        protected override bool Enter()
        {
            base.Enter();
            //Camera cam = ZTCamera.Instance.MainCamera;
            //ZTCamera.Instance.SwitchCameraEffect(ECameraType.SHAKE, cam, null);
            return true;
        }
    }
}