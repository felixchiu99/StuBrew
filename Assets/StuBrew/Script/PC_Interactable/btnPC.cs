using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnPC : InteractableBase, IClickable
{
    Autohand.PhysicsGadgetButton btn;
    new void Start()
    {
        base.Start();
        if (!btn)
            btn = GetComponent<Autohand.PhysicsGadgetButton>();
    }
    public void OnClick()
    {
        transform.localPosition = new Vector3(0, -btn.threshold + 0.01f, 0) ;
    }
}
