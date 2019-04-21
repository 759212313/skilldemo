using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActorBehavior : MonoBehaviour
{
    public Actor Owner { get; set; }
    [SerializeField]
    public ActorBehavior Target;
    [SerializeField]
    public FSMState  FSM;
    [SerializeField]
    public EAIState  AIState;
    [SerializeField]
    public EBattleCamp  Camp;
    //[SerializeField]
    //public ActorAttr Attrs;

    public virtual void SetOwner(Actor owner)
    {
        this.Owner = owner;
    }

    void Update()
    {
        if (this.Owner == null)
        {
            return;
        }
        this.Owner.Step();
    }

    void OnDestroy()
    {
        if (this.Owner == null)
        {
            return;
        }
        this.Owner.Destroy();
        this.Owner = null;
    }
}
