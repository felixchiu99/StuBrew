using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    // Start is called before the first frame update
    protected void Start()
    {
        gameObject.tag = "Interactable_PC";
    }

    void OnInteract()
    {
        //freeze
    }
}
