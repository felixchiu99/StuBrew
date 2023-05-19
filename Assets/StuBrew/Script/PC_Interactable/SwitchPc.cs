using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPc : InteractableBase, IClickable
{
    PhysicsGadgetSwitch physicSwitch;

    void Start()
    {
        base.Start();
        if(!physicSwitch)
            physicSwitch = GetComponent<PhysicsGadgetSwitch>();
    }
    public void OnClick()
    {
        //Debug.Log("Clicked on switch");
        physicSwitch.ForceState(!physicSwitch.GetState());

    }
}
