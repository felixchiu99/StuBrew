using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPullOnClickPC : InteractableBase, IKeyHoldable, IClickable
{
    [SerializeField] float forceMultiplier = 1000;
    [SerializeField] float clickForceMultiplier = 6000;
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 direction = new Vector3(0, 0, 90);
    [SerializeField] Vector3 positionOffset = new Vector3(0, 1, 0);

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

    public void OnHold()
    {
        ApplyForce(forceMultiplier);
    }

    void ApplyForce(float force)
    {
        Vector3 relativeDirection = transform.TransformDirection(direction);
        rb.AddForceAtPosition(relativeDirection.normalized * force, transform.position);
    }
}
