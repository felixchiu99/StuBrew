using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTag))]
public class PickUpPC : Highlightable, IPickup
{
    [SerializeField] float holdDist = 0.5f;
    new protected void Start()
    {
        base.Start();
        gameObject.tag = "Pickupable_PC";
    }
    public void OnInteract()
    {
        Debug.Log("Clicked on pick up able");
    }
    public void OnScroll()
    {
        Debug.Log("scroll");
    }
    public void OnLeft()
    {
        Debug.Log("Left");
    }
    public void OnRight()
    {
        Debug.Log("Right");
    }

    public float GetHoldDist()
    {
        return holdDist;
    }
}

