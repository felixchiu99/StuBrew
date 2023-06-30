using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandlePC : InteractableBase, IClickable
{
    [SerializeField] Rigidbody doorRb;

    [SerializeField] float clickForceMultiplier = 6000;
    [SerializeField] Vector3 handleDirection = new Vector3(0, 0, 90);
    [SerializeField] Vector3 handlePositionOffset = new Vector3(0, 1, 0);

    [SerializeField] float openForceMultiplier = 100;
    [SerializeField] Vector3 openDirection = new Vector3(0, 0, 90);
    [SerializeField] Vector3 openPositionOffset = new Vector3(0, 1, 0);
    Rigidbody rb;
    new void Start()
    {
        base.Start();
        if (TryGetComponent<Rigidbody>(out Rigidbody temp))
        {
            rb = temp;
        }
    }
    public void OnClick()
    {
        ApplyForce(handleDirection, handlePositionOffset, clickForceMultiplier);
        if(doorRb)
            doorRb.isKinematic = false;
        ApplyForce(openDirection, openPositionOffset, openForceMultiplier);
    }
    void ApplyForce(Vector3 direction, Vector3 posOffset, float force)
    {
        Vector3 relativeDirection = transform.TransformDirection(direction);
        rb.AddForceAtPosition(relativeDirection.normalized * force, transform.position + posOffset);
    }
}
