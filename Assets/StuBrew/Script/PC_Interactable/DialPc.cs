using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialPc : InteractableBase, IClickable, IRange
{
    [SerializeField] int stepAmount = 45;
    [SerializeField] bool inverseDir = false;

    public void OnClick()
    {
        //Debug.Log("Clicked on dial");
    }
    public void OnLeft()
    {
        //Debug.Log("dial Left");
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += stepAmount * (inverseDir ? 1 : -1);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
    public void OnRight()
    {
        //Debug.Log("dial Right");
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += stepAmount * (inverseDir ? -1 : 1);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
