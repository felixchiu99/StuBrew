using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : Highlightable
{
    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
        gameObject.tag = "Interactable_PC";
    }

    void OnInteract()
    {
        //freeze
    }
}
