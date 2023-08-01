using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTag))]
public class PickUpPC : Highlightable, IPickup
{
    [SerializeField] float holdDist = 0.7f;
    new protected void Start()
    {
        base.Start();
        gameObject.tag = "Pickupable_PC";
    }
    public void OnInteract()
    {
        if (transform.parent != null)
            if (transform.parent.gameObject.TryGetComponent(out Autohand.PlacePoint placePoint))
            {
                if (gameObject.TryGetComponent(out Autohand.Grabbable obj))
                {
                    placePoint.Remove();
                    transform.SetParent(null);
                    Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                }
            }
    }
    public void OnRelease()
    {
        
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

