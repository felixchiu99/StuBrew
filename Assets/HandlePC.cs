using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePC : InteractableBase, IClickable
{
    [SerializeField] float clickForceMultiplier = 6000;
    [SerializeField] Vector3 direction = new Vector3(0, 0, 90);
    [SerializeField] Vector3 positionOffset = new Vector3(0, 1, 0);

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
        ApplyForce(clickForceMultiplier);
    }
    void ApplyForce(float force)
    {
        Vector3 relativeDirection = transform.TransformDirection(direction);
        rb.AddForceAtPosition(relativeDirection.normalized * force, transform.position);
    }
}
