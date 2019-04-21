using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;

namespace BT
{
    public class BTTask : Action
    {
        public Actor Owner;

        protected override bool Enter()
        {
            if(Owner==null)
            {
                Owner = BTTreeManager.Instance.GetOwnerByNode(this);
            }
            return true;
        }
    }
} 