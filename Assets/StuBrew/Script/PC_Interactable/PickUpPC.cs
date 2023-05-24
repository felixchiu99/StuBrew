using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTag))]
public class PickUpPC : MonoBehaviour, IPickup
{
    protected void Start()
    {
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
}

