using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : Autohand.Demo.Spinner
{
    public Vector3 targetSpeed;
    void Start() {
        StopSpin();
    }

    public void StartSpin()
    {
        rotationSpeed = targetSpeed;
    }

    public void StopSpin()
    {
        rotationSpeed = new Vector3(0,0,0);
    }
}
