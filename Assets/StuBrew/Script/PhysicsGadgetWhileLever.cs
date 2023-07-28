using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsGadgetWhileLever : Autohand.PhysicsGadgetLever
{
    public UnityEvent LargerMid;
    public UnityEvent LowerMid;

    new protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (value + threshold >= 1)
        {
            LargerMid?.Invoke();
        }
        if (value - threshold <= -1)
        {
            LowerMid?.Invoke();
        }
    }
}
